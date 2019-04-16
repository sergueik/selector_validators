### Info

These projects exercises the grammar checks of string to recognize it a valid __cssSelector__ or a valid __XPath__. 
The token construction was based on utilities found in
[CSS Selector to XPath Converter Java tool](https://github.com/sam-rosenthal/java-cssSelector-to-xpath) and simplifid since no deep capturing is needed for validation, 
just a grammar end state. Later the XPath tokenization grammar was created based on the cssSelector one 
(no token aliadation exist at).

### Grammar 

The regexp below is all that is needed tovalidate the 
  * cssSelector
```java
TOKEN_EXTRACTOR = "^([^ ~+>\\[]*(?:\\[[^\\]]+\\])*)($|\\s*[ ~+>]\\s*[^ ~+>\\[].*$)";
```
```java
CSS_ATTRIBUTE_CONDITION_EXTRACTOR = "(?i)^(-?[_a-z]+[_a-z0-9-]*|\\*)?(#[_a-z0-9-]*)?(\\.[_a-z0-9-]*)?(:[a-z][a-z\\-]*\\([^)]+\\))?(\\[\\s*-?[_a-z]+[_a-z0-9-]*\\s*(\\=|\\~=|\\|=|\\^=|\\$=|\\*=)?\\s*([\"'][-_.#a-z0-9:\\/ ]+[\"']|[-_.#a-z0-9:\\/]+)?\\s*\\])*$";
```
  * Xpath
```java
TOKEN_EXTRACTOR = "^\\s*(/?/?\\s*[^ /\\[]+(?:\\[[^\\]]+\\])*)($|\\s*//?\\s*[^ /\\[]+.*$)";
```
### TODO

The cssSelector validator does not recognize atttibute negation semantics, often found in locating of invisible elements in the page:

```java
input:not([class*='disabled'])
```
or
```java
#id:not([class*='disabled'])
```
### License
This project is licensed under the terms of the MIT license.

### Author
[Serguei Kouzmine](kouzmine_serguei@yahoo.com)
