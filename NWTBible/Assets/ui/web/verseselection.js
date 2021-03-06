﻿var selectedVerse;
var verseCollection = [];

function PageOnLoad() {
    // Handle onclick event
    var spans = document.getElementsByTagName("span");
    //var spans = document.getElementsByClassName("verse");
    for (var i = 0; i <= spans.length; i++) {
        spans[i].onclick = OnVerseClick;
    }

    Ti.App.fireEvent('app:numVerses', { numVerses: spans.length });
}

function ConnectHighlighting() {
    var spans = document.getElementsByTagName("span");
    //var spans = document.getElementsByClassName("verse");
    for (var i = 0; i <= spans.length; i++) {
        spans[i].onclick = OnVerseClick;
    }
}

function SwitchFont() {
    var spans = document.getElementsByTagName("span");
    //var spans = document.getElementsByClassName("verse");
    for (var i = 0; i <= spans.length; i++) {
        spans[i].style.fontFamily = "Times New Roman";
    }
}

function DisconnectHighlighting() {
    var spans = document.getElementsByTagName("span");
    //var spans = document.getElementsByClassName("verse");
    for (var i = 0; i <= spans.length; i++) {
        spans[i].onclick = null;
    }
}

function DisplaySelectedVersers() {
    // Loop through the array of stored verses
    for (var i = 0; i < verseCollection.length; i++) {
        alert(verseCollection[i].n + ': ' + verseCollection[i].t);
    }
}

function OnVerseClick() {
    // Select the verse
    selectedVerse = this;

    // Do this . . .
    //this.style.backgroundColor = (this.style.backgroundColor == 'cyan') ? 'transparent' : 'cyan';

    // Or this . . .
    if (selectedVerse.style.backgroundColor == 'yellow') {
        // The verse number of the selected verse of type int
        //var verseNumber = parseInt(selectedVerse.getElementsByClassName("vsAnchor")[0].innerText);
        var verseNumber = parseInt(selectedVerse.id);
        var verseNumberIndex;

        for (var x in verseCollection) {
            if (verseNumber == verseCollection[x].n) {
                verseNumberIndex = verseCollection.indexOf(verseCollection[x]);
            }
        }

        var len = verseCollection.length;

        // Remove the highlight of the unselected verse
        selectedVerse.style.backgroundColor = 'transparent';
        selectedVerse.removeAttribute('style');

        // Remove the unselected verse from the array
        verseCollection.splice(verseNumberIndex, 1);

        // Sort entire array numerically by verse number
        verseCollection.sort(function (a, b) {
            return a.n - b.n
        });

        // Calling C# code
        VerseSelection.SubtractVerse(verseNumber);
    }
    else {
        selectedVerse.style.backgroundColor = 'yellow';
        //alert(selectedVerse.innerHTML);
        //alert(selectedVerse.textContext);
        //alert(selectedVerse.innerText);
        var verseNumber = selectedVerse.id;
        var verseText = selectedVerse.innerText.substring(verseNumber.length + 1);
        //alert(verseNumber);
        //alert(verseText);

        // Stores the selected verse in an array
        verseCollection.push({
            n: verseNumber,
            t: verseText
        });

        //alert(verseCollection[0].t);

        // Sort entire array numerically by verse number
        verseCollection.sort(function (a, b) {
            return a.n - b.n
        });

        // Calling C# code
        VerseSelection.ClearVerses();
        for (var v in verseCollection) {
            VerseSelection.AddVerse(verseCollection[v].n, verseCollection[v].t);
        }
    }
}

function HighlightVerse(id) {
    // Select the verse
    selectedVerse = document.getElementById(id);

    selectedVerse.style.backgroundColor = 'yellow';

    var verseNumber = selectedVerse.id;
    var verseText = selectedVerse.innerText.substring(verseNumber.length + 1);

    // Stores the selected verse in an array
    verseCollection.push({
        n: verseNumber,
        t: verseText
    });

    // Sort entire array numerically by verse number
    verseCollection.sort(function (a, b) {
        return a.n - b.n
    });

    //Ti.App.fireEvent('app:collectSelectedVerses', { data: verseCollection });
}

function ChangeFontSize(size) {
    //alert(Math.floor(size));

    // For Bible reader
    var elements = document.getElementsByClassName("page");
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.fontSize = size + "px";
    }

    // For Publication reader
    elements = document.getElementsByClassName("body");
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.fontSize = size + "px";
    }
}

function ScrollToHighlight(id) {
    var verse = document.getElementById(id);

    function findPos(obj) {
        var curtop = 0;
        if (obj.offsetParent) {
            do {
                curtop += obj.offsetTop;
            } while (obj = obj.offsetParent);
            return [curtop];
        }
    }

    window.scroll(0, findPos(verse));

    //verse.scrollIntoView(true);
}

Ti.App.addEventListener("app:clearSelected", function (event) {
    ClearSelection();
});

Ti.App.addEventListener("app:highlightVerse", function (event) {
    HighlightVerse(event.id);
});

Ti.App.addEventListener("app:executeScripturePrepare", function (event) {
    getBookChapterVerses(event.books, event.abbrbooks)
});

Ti.App.addEventListener("app:scrollToVerse", function (event) {
    var verse = document.getElementById(event.id);

    function findPos(obj) {
        var curtop = 0;
        if (obj.offsetParent) {
            do {
                curtop += obj.offsetTop;
            } while (obj = obj.offsetParent);
            return [curtop];
        }
    }

    window.scroll(0, findPos(verse));
});

function ClearSelection() {
    verseCollection = [];

    var spans = document.getElementsByTagName("span");

    for (var i = 0; i <= spans.length; i++) {
        if (spans[i].style.backgroundColor == 'yellow') {
            spans[i].removeAttribute('style');
            spans[i].style.backgroundColor = 'transparent';
        }
    }
}

// All the Bible Books
//var books = ["Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy", "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings", "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah", "Esther", "Job", "Psalm", "Proverbs", "Ecclesiastes", "Song of Solomon", "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk", "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark", "Luke", "John", "Acts", "Romans", "1 Corinthians", "2 Corinthians", "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians", "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon", "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John", "3 John", "Jude", "Revelation"];
//var abbrbooks = ["Gen.", "Ex.", "Lev.", "Num.", "Deut.", "Josh.", "Judges", "Ruth", "1 Sam.", "2 Sam.", "1 Ki.", "2 Ki.", "1 Chron.", "2 Chron.", "Ezra", "Neh.", "Esther", "Job", "Psalm", "Prov.", "Eccl.", "Song of Solomon", "Isa.", "Jer.", "Lam.", "Ezek.", "Dan.", "Hosea", "Joel", "Amos", "Obadiah", "Jonah", "Mic.", "Nahum", "Hab.", "Zeph.", "Haggai", "Zech.", "Mal.", "Matt.", "Mark", "Luke", "John", "Acts", "Rom.", "1 Cor.", "2 Cor.", "Gal.", "Eph.", "Phil.", "Col.", "1 Thess.", "2 Thess.", "1 Tim.", "2 Tim.", "Titus", "Phil.", "Heb.", "Jas.", "1 Pet.", "2 Pet.", "1 John", "2 John", "3 John", "Jude", "Rev."];


// The scripture clicked in the webview
var element;

var xbook;
var xchapter;
var xverses;

function handlehref(item) {
    // The scripture clicked in the webview
    element = item;

    //getBookChapterVerses(item.innerText); 
    Ti.App.fireEvent('app:prepScriptureFromPub');
}

function getBookChapterVerses(books, abbrbooks) {
    var data = element.innerText;

    Ti.API.info(data);

    // THIS
    // var guyman = JSON.parse(Ti.App.Properties.getString("guyman"));
    // var books = guyman.data.biblebooktitles;
    // var abbrbooks = guyman.data.biblebookabbrs;

    // OR
    // THIS		
    //var books = Guyman.data.biblebooktitles;
    //var abbrbooks = Guyman.data.biblebookabbrs;

    // The static original that will never change, split two parts, first Book+Chapter, second Verses
    var original = [];
    // Split two parts, first Book+Chapter, second Verses
    var scripture;
    // The static original with no spaces in the beginning
    var clean = data.replace(/^\s+/, "");

    // If the element contains a colon, meaning Chapter and Verse
    if (element.innerText.match(/[:]/)) {
        scripture = data.replace(/[:]/, '\x01').split('\x01');
        original = data.replace(/[:]/, '\x01').split('\x01');
    }
        // If the element does not contain a colon, meaning only Verses
    else {
        // Search backward until a Chapter is found, make that element current, and save the Verses from the original element
        while (!element.innerText.match(/[:]/)) {
            element = element.previousSibling;
        }
        data = element.innerText;
        scripture = data.replace(/[:]/, '\x01').split('\x01');
        original = data.replace(/[:]/, '\x01').split('\x01');
        original[1] = clean;
    }

    // Book and Chapter, remove any spaces or semi-colons
    var bookandchapter = scripture[0].replace(/^\s+/, "").replace(";", "");
    // Book name only, remove space and Chapter number
    var name = bookandchapter.replace(/( \d+)(?!.*\d)/, "");

    if (name == "Psalm") {
        name = "Psalms";
    }

    // Loop through each Bible Book to match the selected scripture Book
    for (var i = 0; i < books.length; i++) {
        // If current Bible Book matches the Book name
        if (books[i] == name) {
            // Chapter number, removing the Book name and any spaces
            xchapter = bookandchapter.substring(books[i].length, bookandchapter.length).replace(/^\s+|\s+$/g, "");
            // Current Bible Book
            xbook = books[i];

            // If everything is correct, break from the loop
            break;
        }
            // If there is no match at first
        else {
            // If scripture contains a Bible Book
            if (element.innerText.match(/[A-Z]/)) {
                // Continue to the next Bible Book in the loop
                continue;
            }
                // If scripture does not contain a Bible Book
            else {
                // Search backward until a Book name is found
                while (!element.innerText.match(/[A-Z]/)) {
                    // The scripture that is BEHIND the scripture clicked in the webview
                    element = element.previousSibling;
                }

                // Set data to the scripture found that is BEHIND the scripture clicked
                data = element.innerText.replace(/^\s+/, "");
                // Reset to the new data, this is for retrieving the Book name if the current scripture does not have one			
                scripture = data.replace(/[:]/, '\x01').split('\x01');

                // The new Book name
                var tempbook = scripture[0].replace(/^\s+/, "").replace(";", "").replace(/( \d+)(?!.*\d)/, "");

                // Book and Chapter, remove any spaces or semi-colons
                bookandchapter = tempbook + ' ' + original[0].replace(/^\s+/, "").replace(";", "");
                // Book name only, remove space and Chapter number
                name = bookandchapter.replace(/( \d+)(?!.*\d)/, "");

                if (name == "Psalm") {
                    name = "Psalms";
                }

                // If current Bible Book matches the Book name
                if (books[i] == tempbook) {
                    // Chapter number, removing the Book name and any spaces
                    xchapter = bookandchapter.substring(tempbook.length, bookandchapter.length).replace(/^\s+|\s+$/g, "");
                    // Current Bible Book
                    xbook = name;

                    // If everything is correct, break from the loop
                    break;
                }

                // If there is no match at all, resets everything and continues to the next Bible Book in the loop
                continue;
            }
        }
    }

    // Set and format the verses into an array
    xverses = versesToArray(original[1]);

    //alert("Book: " + xbook + ", Chapter: " + xchapter + ", Verses: " + xverses);

    // Prepare the scripture object
    var scripture = {
        book: xbook,
        chapter: xchapter,
        verses: xverses
    };

    // If there are good formatted Verses in the array
    if (xverses !== 0) {
        // Launch the Bible and go to the selected scripture
        Ti.App.fireEvent("app:pubToBible", { data: scripture });
    }
        // If there are multiple Chapters in the array
    else if (xverses === 0) {
        alert('SORRY! You cannot view multiple chapters at the same time.');
    }
        // If there are no Verses in the array
    else {
        alert('SORRY! This action is not possible.');
    }
}

function checkValidCharacters(strString) {
    // Check for valid numeric strings
    var strValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    var strChar;
    var blnResult = true;

    if (strString.length === 0) return false;

    // Test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult; i++) {
        strChar = strString.charAt(i);
        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }
    return blnResult;
}

function versesToArray(verses) {
    // Remove semi-colon and any letters that might be in the Verses
    var collection = verses.replace(";", "").replace(/[A-Za-z]/g, "");

    // Array to hold each Verse number
    var data = [];

    var first;
    var single;
    var temp;
    var increment;
    var num;

    // ODDLY FORMATTED VERSES
    // If there is a comma that seperates any Verses (oddly formatted Verses)
    if (verses.match(/, /)) {
        // Make an array of each element that comes before a comma
        first = collection.split(/[,]\s+?/g)

        // Loop through each element
        for (var i in first) {
            // Remove any commas
            single = first[i].replace(",", "");
            // If a colon exists in any element, then that means there is more than one Chapter
            // Return 0
            if (single.match(/:/)) {
                return 0;
            }
            // If there exists a dash, than that means multiple Verses
            if (single.match(/-/)) {
                // Make an array of the first Verse and the last Verse
                temp = single.split("-");
                // Find the distance between the Verses in the array
                increment = temp[1] - temp[0];

                // Loop through each Verse number
                for (var j = 0; j < increment + 1; j++) {
                    // The Verse number
                    num = parseInt(temp[0], 10) + j;
                    // Push the Verse number into the return array
                    data.push(num.toString());
                }
            }
                // If there is not a dash, than that means only one Verse
            else {
                // Push the Verse number into the return array
                data.push(single);
            }
        }
    }
        // REGULARLY FORMATTED VERSES
        // If there is no separation (regularly formatted Verses)
    else {
        // Remove commas and any spaces that might be in the Verses
        single = collection.replace(",", "").replace(/^\s+|\s+$/g, "");
        // If a colon exists in the element, then that means there is more than one Chapter
        // Return 0
        if (single.match(/:/)) {
            return 0;
        }
        // If there exists a dash, than that means multiple Verses
        if (single.match(/-/)) {
            // Make an array of the first Verse and the last Verse
            temp = single.split("-");
            // Find the distance between the Verses in the array		    
            increment = temp[1] - temp[0];
            // Loop through each Verse number
            for (var k = 0; k < increment + 1; k++) {
                // The Verse number
                num = parseInt(temp[0], 10) + k;
                // Push the Verse number into the return array
                data.push(num.toString());
            }
        }
            // If there is not a dash, than that means only one Verse
        else {
            // Push the Verse number into the return array
            data.push(single);
        }
    }

    return data;
}