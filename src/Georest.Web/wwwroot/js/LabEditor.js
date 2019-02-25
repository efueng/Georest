﻿/*===============================================================
   LabEditor.js
   JS file LabEditor relies on to function.

   Throughout this file are calls to a 'contentEditor' function
   that is really a wrapper around the 'summernote' function because
   our group agreed that in case of licensing issues and profiting
   off this program, then ContentEditor.js should be modified
   to have a different text editor from summernote. If the team
   deems this to be too speculative and wants to revert the code
   back to summernote calls, then we have provided commented-out
   versions of the summernote calls.
  ===============================================================*/
var numExercises = 0;
var lab;

var labElements = ['remove-ex-',
    'move-up-ex-',
    'move-down-ex-',
    'ex-title-',
    'summernote',
    'ex-response-',
    'multi-choice-',
    'ex-multi-',
    'add-multi-option-',
    'remove-multi-option-',
    'ex-group-',
    'exercises-group-',
    'create-exercise-btn-'
];

$(document).ready(function () {
    $("#backIcon").attr("href", "../LabManagerView");
    $(".modal").off();
    $(".modal").on('show.bs.modal', function () {
        $(".modal-dialog").css("margin-top", $(window).height() / 3).css("background", "white").css("border-radius", 4);
    });
    $("#summernote-intro").contentEditor(); //$("#summernote-intro").summernote();
    var needsSave = false;
    $("#lab-title").val(window.document.title);
    $("#brand-title").val(window.document.title);

    /*=============================================================
     * Event Listener that updates page title, navbar when title is
     * changed. This may be better suited at save instead of real
     * time.
     ==============================================================*/
    $(".title").keyup(function () {
        needsSave = true;
        var newTitle = $("#lab-title").val();
        $("#brand-title").html(htmlspecialchars(newTitle));
        window.document.title = newTitle;
    });

    /*=============================================================
     * Event Listener for creating a new exercise.
     ==============================================================*/
    $("#create-exercise-btn").off();
    $("#create-exercise-btn").click(function () {
        numExercises++;
        createNewExercise(numExercises);
        needsSave = true;
    });
    /*=============================================================
     * Event Listener for the save lab button. 
     * - Pulls all content from the page, loads it into the variable labData 
     * - Package labData as JSON and send it to LabController.cs via AJAX 
     ==============================================================*/
    $("#save-lab-btn").off();
    $("#save-lab-btn").click(function () {
        var title = $("#lab-title").val();
        var intro = $("#summernote-intro").contentEditor('content'); // $("#summernote-intro").summernote('code');
        var exerciseTitles = [];
        var exerciseContent = [];
        var exerciseResponses = [];
        store(exerciseTitles, exerciseContent, exerciseResponses, '', numExercises);

        console.log(title);
        console.log(exerciseTitles);
        console.log(exerciseContent);
        console.log(exerciseResponses);

        var labData = JSON.stringify({
            'title': title,
            'intro': intro,
            'exerciseTitles': exerciseTitles,
            'exerciseContent': exerciseContent,
            'exerciseResponses': exerciseResponses,
            'name': meta.Name,
            'key': meta.LabID,
            'created': meta.DateTimeCreated,
            'due': due,
            'isPublished': isPublished,
            'publishDate': publishDate
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../../Lab/Save',
            data: labData,
            success: function (response, textStatus, jqHXR) {
                modalAlert("Lab was saved successfully!", "Lab Saved");
                $('#preview-lab-btn').prop('disabled', false);
            },
            error: function (jqHXR, textStatus, errorThrown) {
                console.log("The following errors occurred when saving the lab: " + textStatus, errorThrown);
            }
        });
    });
    $("#preview-lab-btn").off();
    $("#preview-lab-btn").click(function () {
        window.open('../../Lab/DisplayLab/' + meta.LabID);
    });
    // load the lab selected in LabManager
    loadLab(lab);
}); // Document Ready
// handle storing exercises while saving a lab, including nested exercises
function store(exerciseTitles, exerciseContent, exerciseResponses, id, count) {
    for (var i = 1; i <= count; i++) {
        var exerciseTitle = htmlspecialchars($("#ex-title-" + id + i).val());
        exerciseTitles.push(exerciseTitle);
        var content = $("#summernote" + id + i).contentEditor('content'); // $("#summernote" + id + i).summernote('code');
        exerciseContent.push(content);
        var exerciseResponse = $("#ex-response-" + id + i).val();
        if (exerciseResponse != 'group') {
            var tag = $("<div class='row response'/>").attr("id", "response-" + id + i).addClass(exerciseResponse);
            var tagIn = $("<div class='col-md-10'/>").appendTo(tag);
            storage[exerciseResponse](tagIn, exerciseTitle, id + i);
            exerciseResponses.push($("<div/>").append(tag).html());
        }
        else {
            var len = $('#exercises-group-' + id + i).children().length;
            exerciseResponses.push(len);
            store(exerciseTitles, exerciseContent, exerciseResponses, id + i + '_', len);
        }
    }
}
// load exercises into the editor
function loadExercises(exlist, id) {
    for (var i = 0; i < exlist.length; i++) {
        var index = (id == '') ? (i + 1) : id + '_' + (i + 1); // exercises are 1-index based
        var createbtn = (id == '') ? '#create-exercise-btn' : '#create-exercise-btn-' + id;
        $(createbtn).click();
        $("#ex-title-" + index).val(htmlspecialchars_decode(exlist[i].ExerciseTitle));
        $("#summernote" + index).contentEditor('content', exlist[i].Content); // $("#summernote" + index).summernote('code', exlist[i].Content);
        if (exlist[i].Response != '') {
            var t = 'short';
            if (exlist[i].Response != null) {
                var response = $(exlist[i].Response); // convert response back into a <div> tag
                t = response[0].classList[2];
            }
            $("#ex-response-" + index).val(t);
            if (t == 'multi' || t == 'many' || t == 'dropMulti' || t == 'dropMany') {
                var mc = $("#multi-choice-" + index).show();
                var s = "option"
                if (t == 'multi' || t == 'many')
                    s = "input[name='multi-" + index + "']";
                var res = $(s, response);
                for (var k = 1; k <= res.length; k++) {
                    if (k >= 3)
                        $("#add-multi-option-" + index).click();
                    var selector = "input[name='multi-" + k + "']";
                    $(selector, mc).val(res[k - 1].value);
                }
            }
        }
        else {
            $('#ex-response-' + index).val('group');
            $('#ex-group-' + index).show();
            loadExercises(exlist[i].ExerciseList, index);
        }
    }
}
// load lab data into the editor
function loadLab(lab) {
    $("#lab-title").val(lab.Title);
    $("#summernote-intro").contentEditor('content', lab.Intro); // $("summernote-intro").summernote('code', lab.Intro);
    loadExercises(lab.ExerciseList, '');
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../../Lab/CheckLabName',
        data: JSON.stringify({ 'name': lab.Title }),
        success: function (response, textStatus, jqHXR) {
            $('#preview-lab-btn').prop('disabled', true);
        }
    });
    /*if (lab.Intro == "" && lab.ExerciseList.length == 1
        && lab.ExerciseList[0].ExerciseTitle == 'Test Exercise'
        && lab.ExerciseList[0].Content == 'Lorem Ipsum')
        */
}
/*=============================================================
 * Adds proper event listeners.
 ==============================================================*/
function addExerciseEvents(id, html) {
    create(id, 'ex-', labElements);

    var d = html.parent().data('depth');
    $('#exercises-group-' + id).data('depth', d + 1);
    $("#remove-ex-" + id).off();
    $("#remove-ex-" + id).on('click', function () {
        var exId = this.id.split('-');
        var exnum = exId[exId.length - 1];
        var end = exnum.lastIndexOf('_');
        var func = function (event) {
            var exnum = event.data.exnum;
            var end = event.data.end;
            if (end == -1) {
                numExercises--;
                removeExercise(exnum, '.exercises');
            }
            else
                removeExercise(exnum, '#exercises-group-' + exnum.substring(0, end));
        };
        if ($('#ex-response-' + exnum).val() != 'group')
            func({ data: { 'exnum': exnum, 'end': end } });
        else
            modalConfirm({
                msg: "The exercises in this group exercise will be removed. Press 'OK' to continue.",
                ok: func,
                okData: { 'exnum': exnum, 'end': end }
            });
    });
    $("#move-up-ex-" + id).off();
    $("#move-up-ex-" + id).on('click', function () {
        var up = true;
        var exId = this.id.split('-');
        var exnum = exId[exId.length - 1];
        var depth = $(this).parent().parent().parent().data('depth');
        moveExercise(exnum, depth, up);
    });
    $("#move-down-ex-" + id).off();
    $("#move-down-ex-" + id).on('click', function () {
        var up = false;
        var exId = this.id.split('-');
        var exnum = exId[exId.length - 1];
        var depth = $(this).parent().parent().parent().data('depth');
        moveExercise(exnum, depth, up);
    });
    $('select').off();
    $('select').on('change', function () {
        mutexLock(function (args) {
            var id = $(args).attr('id').split('-');
            var exNum = id[id.length - 1];
            var func = function (event) {
                var exNum = event.data.exNum;
                var value = event.data.value;
                if (value == "multi" || value == "many" || value == "dropMulti" || value == "dropMany")
                    $("#multi-choice-" + exNum).show();
                else
                    $("#multi-choice-" + exNum).hide();
                if (value == 'group')
                    $("#ex-group-" + exNum).show();
                else {
                    $("#ex-group-" + exNum).hide();
                    var len = $('#exercises-group-' + exNum).children().length;
                    while (len > 0) {
                        removeExercise(exNum + '_' + len, '#exercises-group-' + exNum + '_' + len);
                        len--;
                    }
                }
            };
            if (args.value == 'group' || $('#ex-group-' + exNum).is(':hidden'))
                func({ data: { exNum: exNum, value: args.value } });
            else
                modalConfirm({
                    msg: "The exercises in this group exercise will be removed. Press 'OK' to continue.",
                    ok: func,
                    okData: { 'value': args.value, 'exNum': exNum },
                    cancel: function (event) { event.data.args.value = 'group'; },
                    cancelData: { 'args': args }
                });
        }, 'select', this);
    });
    $("#add-multi-option-" + id).off();
    $("#add-multi-option-" + id).on('click', function () {
        var exNum = this.parentElement.id.split('-')[2];
        var multi = $("#ex-multi-" + exNum);
        var opts = $(multi).children();
        var numOpts = opts.length / 2;
        $("<label/>").html("Option " + (++numOpts) + ":").appendTo(multi); //jQuery assembly implemented
        $("<input/>").attr("type", "text").attr("name", "multi-" + numOpts).appendTo(multi);
        if (numOpts > 4) // Max of 5 options
            $("#add-multi-option-" + exNum).attr("disabled", true);
        if (numOpts > 2) // Min of 2 options
            $("#remove-multi-option-" + exNum).attr("disabled", false);
    });
    $("#remove-multi-option-" + id).off();
    $("#remove-multi-option-" + id).on('click', function () {
        var exNum = this.parentElement.id.split('-')[2];
        var multi = $("#ex-multi-" + exNum);
        var opts = $(multi).children();
        var numOpts = opts.length / 2;
        if (!document.documentMode) {
            opts[opts.length - 1].remove();
            opts[opts.length - 2].remove();
        }
        else { // for IE compatibility
            opts[opts.length - 1].removeNode(true);
            opts[opts.length - 2].removeNode(true);
        }
        numOpts--;
        if (numOpts < 3) // Min of 2 options
            $("#remove-multi-option-" + exNum).attr("disabled", true);
        if (numOpts < 5) // Max of 5 options
            $("#add-multi-option-" + exNum).attr("disabled", false);
    });

    $("#summernote" + id).contentEditor(); // $("#summernote" + id).summernote();
}
/*============================================
  Adds a new exercise to the exercises div
============================================*/
function createNewExercise(exerciseNum) {
    var html = $("#ex-0").clone().prop("hidden", false).appendTo(".exercises").attr("id", "ex-" + numExercises);
    addExerciseEvents(exerciseNum, html);
    $("#create-exercise-btn-" + numExercises).off();
    $("#create-exercise-btn-" + numExercises).on('click', function () {
        var exNum = this.id.split('-')[3];
        var newId = $('#exercises-group-' + exNum).children().length + 1;
        var id = exNum + '_' + newId;
        var html = $("#ex-0").clone().prop("hidden", false).appendTo("#exercises-group-" + exNum).attr("id", "ex-" + exNum + '_' + newId);
        addExerciseEvents(id, html);
        $("option[value='group']", html).remove();
        $('#exercises-group-' + id).remove();
    });
}
/*=============================================================
 * Removes an exercise from the exercises div
 * - Destroys the related summernote instance
 * - Adjusts exercise id's so that order is preserved
 ==============================================================*/
function removeExercise(exerciseNum, selector) {
    contentEditorClean(exerciseNum);
    $('#ex-' + exerciseNum).remove();
    var exercises = $(selector).children();
    var s = exerciseNum.split('_');
    exerciseNum = s[s.length - 1];
    var i = (exerciseNum > 1) ? exerciseNum - 1 : 0;
    for (i; i < exercises.length; i++) {
        var oldNum = exercises[i].id.split('-')[1]; // I don't like this, but it'll do for now
        s = oldNum.split('_');
        var newNum = (parseInt(s[s.length - 1]) - 1);
        for (var j = s.length - 2; j >= 0; j--)
            newNum = s[j] + '_' + newNum;
        exercises[i].id = "ex-" + newNum;
        changeExercise(oldNum, newNum);
    }
}
/*==============================================
  Clears summernotes within 'nested' exercises
==============================================*/
function contentEditorClean(exerciseNum) {
    $('#summernote' + exerciseNum).contentEditor('destroy'); // $('#summernote' + exerciseNum).summernote('destroy');
    var children = $('#exercises-group-' + exerciseNum).children();
    for (var i = 1; i <= children.length; i++)
        contentEditorClean(exerciseNum + '_' + i);
}
/*=============================================================
 * Moves an exercise in the exercises div
 * - Adjusts affected ids so that order is preserved
 ==============================================================*/
function moveExercise(exercise, depth, up) {
    var s = exercise.split('_'), newex = s[0];
    var ex = parseInt(s[depth]);
    for (var i = 1; i < depth; i++)
        newex += '_' + s[i];
    var len;
    if (s.length == 1)
        len = $('.exercises').children().length;
    else
        len = $('#exercises-group-' + newex).children().length;
    if (len > 1) {
        if ((ex > 1) && up) {
            s[depth] = ex - 1;
            newex = s[0];
            for (var i = 1; i < s.length; i++)
                newex += "_" + s[i];
            $("#ex-" + exercise).insertBefore("#ex-" + newex);
            swapExercises(exercise, newex);
        }
        else if ((ex < len) && !up) {
            s[depth] = parseInt(ex) + 1;
            newex = s[0];
            for (var i = 1; i < s.length; i++)
                newex += "_" + s[i];
            $("#ex-" + newex).insertBefore("#ex-" + (exercise));
            swapExercises(exercise, newex);
        }
    }
}
/*=============================================================
 * Change related child id's of ex-oldNum to represent newNum.
 ==============================================================*/
function changeExercise(oldNum, newNum) {
    change(oldNum, newNum, labElements);
    var len = $('#exercises-group-' + newNum).children().length;
    for (var i = 1; i <= len; i++)
        changeExercise(oldNum + '_' + i, newNum + '_' + i);
}
/*=============================================================
 * Swap id's and related child id's of ex1 and ex2.
 ==============================================================*/
function swapExercises(ex1, ex2) {
    swap(ex1, ex2, ['ex-']);
    swap(ex1, ex2, labElements);
    var len = Math.max($('#exercises-group-' + ex1).children().length,
        $('#exercises-group-' + ex2).children().length);
    for (var i = 1; i <= len; i++)
        swapExercises(ex1 + '_' + i, ex2 + '_' + i);
}