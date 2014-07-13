using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NWTBible
{
    public class BibleBook
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Chapters { get; set; }
        public string Writer { get; set; }
        public string Place { get; set; }
        public string Completed { get; set; }
        public string Time { get; set; }

        public string WriterAndPlace
        {
            get
            {
                //return "Written by " + Writer + " in " + Place;
                return "——" + Writer;
            }
        }
    }

    public class BibleChapter
    {
        public BibleBook Book { get; set; }
        public string ChapterNumber { get; set; }
        public string BookAndChapter
        {
            get
            {
                return Book.Title + " " + ChapterNumber;
            }
        }
    }

    public class DailyTextDay
    {
        public int Index { get; set; }
        public string Date { get; set; }
        public string DateShort { get; set; }
        public string Scripture { get; set; }
        public string Content { get; set; }
    }

    public class NoteScripture
    {
        public string NWTId { get; set; }
        public string Title { get; set; }
        public string Scripture { get; set; }
        public string NoteTitle { get; set; }
        public string NoteBody { get; set; }
        public string ScriptureForHighlight { get; set; }
        public int Id { get; set; }
        public string Language { get; set; }
        public string Book { get; set; }
        public string Chapter { get; set; }
        public string Verses { get; set; }
    }

    public class BibleVerse
    {
        public BibleBook Book { get; set; }
        public BibleChapter Chapter { get; set; }
        public string VerseNumber { get; set; }
        public string Scripture { get; set; }
        public string BookChapterVerse
        {
            get
            {
                return Book.Title + " " + Chapter.ChapterNumber + ":" + VerseNumber;
            }
        }
        public string ScriptureFormatted
        {
            get
            {
                return BookChapterVerse + "—" + Scripture;
            }
        }
    }

    public class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string EPUBLink { get; set; }
        public string Category { get; set; }
        public int CurrentPage { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public Publication()
        {

        }
    }

    public class PublicationArticle
    {
        public string PublicationTitle { get; set; }
        public string ArticleTitle { get; set; }
        public string DocumentLocation { get; set; }
        public string Content { get; set; }

        public PublicationArticle()
        {

        }
    }

    public class NWTFile
    {
        public string FileName { get; set; }
        public string FileURL { get; set; }
        public string FileTitle { get; set; }

        public NWTFile(string FileName = "", string FileURL = "", string FileTitle = "")
        {
            this.FileName = FileName;
            this.FileURL = FileURL;
            this.FileTitle = FileTitle;
        }
    }

    public class NavPoint
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public int Order { get; set; }
    }
    
    public class ChapterItem
    {
        public string Title {get; set;}
        public string DocumentLocation {get; set;}
        public string Content {get; set;}
    }

    public class BibleChapterItem
    {
        public string Cdata { get; set; }
    }
}