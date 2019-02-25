/*
 * ContentEditor API
 *
 * In order for LabEditor.js to function correctly, there must
 * be an object named 'ContentEditor' which controls how the
 * contents of lab exercises are created, edited, and destroyed.
 * Any implementation of ContentEditor must have these four
 * functions defined and follow these specifications to be valid:
 *
 * ContentEditor.create
 *
 * Initializes the form used to generate lab exercise content.
 * This function must exist even if the implementation does not
 * dynamically add HTML elements.
 *
 * @param  res jQuery result
 * @return res, used to permit jQuery function chaining
 *
 * ContentEditor.destroy
 *
 * Destroys the form used to generate lab exercise content.
 * This function must exist even if the implementation does not
 * dynamically remove HTML elements.
 *
 * @param  res jQuery result
 *
 * ContentEditor.get
 *
 * Retrieve the contents from the form.
 *
 * @param  res jQuery result
 * @return contents of the content editor form
 *
 * ContentEditor.set
 *
 * Assigns new content to the form
 *
 * @param  res jQuery result
 * @param  val content to be assigned
 * @return res, used to permit jQuery function chaining
 */

// current ContentEditor object is implemented using summernote.
var ContentEditor = {
    create: function (res) {
        return res.summernote();
    },
    destroy: function (res) {
        res.summernote('destroy');
    },
    get: function (res) {
        return res.summernote('code');
    },
    set: function (res, val) {
        return res.summernote('code', val);
    }
};

$.fn.extend({
    contentEditor: function () {
        if (arguments.length == 0)
            return ContentEditor.create(this);
        else if (arguments.length == 1) {
            if (arguments[0] == 'destroy')
                ContentEditor.destroy(this);
            else if (arguments[0] == 'content')
                return ContentEditor.get(this);
        }
        else if (arguments.length == 2) {
            if (arguments[0] == 'content')
                return ContentEditor.set(this, arguments[1]);
        }
    }
});