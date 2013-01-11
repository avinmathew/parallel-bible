# Parallel Bible

A three column bible with English, Malayalam and Hindi translations.

This application generates a HTML static site using Razor templates. jQuery Mobile is used (referenced via CDN links) and HTML pages are generated for the table of contents and individual chapters.

The application expects the English translation file, `ESV.xml`, to be in the current directory - this can be downloaded from http://opensong.org/d/downloads#bible_translations. The Malayalam bible data is downloaded from http://malayalambible.in while the Hindi bible data is downloaded from http://gospelgo.com/a/hindi/bible.htm - both of these downloads are cached as XML files. Please contact the respective sites for permission as the download is done through screen scraping.

A comparison of the number of chapters and verses per book across translations is currently commented out in the source code. I left it in there as I found it was interesting that the Indian translations differed in these statistics.
