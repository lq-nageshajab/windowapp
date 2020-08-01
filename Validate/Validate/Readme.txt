1. Print each element with xpath
2. Find mandatory element not present in start and main file(some configuration pending in code)
3. page navigation should not have autoDetect set as action
4. check proxies are present or not
5. Note element having name 'DONOTUSE' should have their Misc-Save Content property always be false

--project properties
6. RunActiveXAndFlash property should be false
7. The Main script should ALWAYS have the “Include Start URLs in data output” checkbox checked

--project advanced properties
8. IgnorePageLoadErrorCodes property should be false
9. IsRefreshAfterPageLoad property should be false

10. Delay after ajax call milliseconds should be set default to 100
11. Delay after completed action milliseconds should be set default to 0
12. Wait for main document redirect should be set to false
13. IsRandomPageLoadDelay should be set to false
14. Min page load dealy should be set to 1000
15. Max page load dealy should be set to 5000
16. IsWaitForAjaxAfterPageLoad should be set to false