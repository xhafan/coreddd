function getTextSelectionIndexesFromTextArea(textArea) {

    var result = {
        startIndex: 0,
        endIndex: 0,
        textLength: 0
    };

    if (textArea.selectionStart != undefined) {
        // Usually firefox.
        result.startIndex = textArea.selectionStart;
        result.endIndex = textArea.selectionEnd;
    }
    else if (textArea.createTextRange) {
        // IE, Opera.
        textArea.focus();
        var EMAILMAKER_STR = "~EMAILMAKER~";
        while (textArea.value.indexOf(EMAILMAKER_STR) != -1) {
            // EMAILMAKER_STR value is a substring - let's modify EMAILMAKER_STR
            EMAILMAKER_STR += "~";
        }
        var range = document.selection.createRange();
        var rangeSize = range.text.length;

        var textAreaValueBackup = textArea.value;
        range.text = EMAILMAKER_STR + range.text;
        result.startIndex = textArea.value.indexOf(EMAILMAKER_STR);
        result.endIndex = result.startIndex + rangeSize;

        textArea.value = textAreaValueBackup;
    }
    result.textLength = textArea.value.length;
    return result;
}