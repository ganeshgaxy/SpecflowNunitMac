Feature: Expedia
	Create a new test that is covering the following scenario
	
    @excercise2
    Scenario: Flight Booking
    	Given Open URL "https://www.expedia.com"
        Then From "BRU" To "New York, NY"
        Then Set the dates for Journey
        Then Set Travelers Adult 1 And Children 1
        Then Search for Results
        Then End the Test