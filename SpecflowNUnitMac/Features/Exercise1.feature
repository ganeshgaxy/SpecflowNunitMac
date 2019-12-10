Feature: Google
    Create a test Automation script which opens google.com and searches for “Bahamas”
    Make sure that your test contains a step that checks you are arrived on the results page
    The test script must take a screenshot from that result page
    Expand the test scenario so it also does a search on “Amsterdam”
    
    @excercise1
    Scenario: Google Search
        Given Open URL "https://www.google.com"
        Then Search for "Bahamas"
        Then Check if Results page is displayed
        Then Take Screenshot
        Then Search for "Amsterdam" again
        Then End the Test
