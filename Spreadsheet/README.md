# Team ValuePack - Spreadsheet GUI

##Personal Information
- Author: Samuel Hancock
- Partner: Hyrum Schenk
- Course: CS3500, University of Utah, School of Computing
- Samuel's GitHub ID: skeevySeeds
- Hyrum's Github ID: schenkhyrum
- [Repository](https://github.com/uofu-cs3500-spring20/assignment-six-completed-spreadsheet-valuepack.git)
- Commit #: b6f87f2e97e858588580a75c2e9fd184509cac70
- Date: Febuary 26, 2020
- Assignment: Assignment 06 - Spreadsheet Front-End Graphical User Interface
- Copyright: CS3500, Samuel Hancock, Hyrum Schenk - This work may not be copied in academic coursework

##Partnership
Samuel Hancock was assigned to add the functionality to the menu bar buttons, build a help page, and
make sure that the help button could reference it. This included reading files and having safety catches
when the files were changed but not saved.

Hyrum Schenk was tasked with providing ineteractive utility for the Gui including clicks, updating Cell contents with 
the appropriate text and related value fields, and handling arrow key entries. Hyrum also took on the responsibility of the
BackGround worker with its associated progress bar.

## Branches
Samuel Hancock branches	|	Hyrum Schenk branches
------------------------|-------------------------
TestingSuite			|	PurgeBranch	
CloseButton-SafetyFeature|	InitialSpreadsheetGui
MenuBar-OpenButton		|	BackGroundThread
						|	Update Dependees


## Comments to Evaluators
- Additional Feature: Above the grid widget, there's a button with "Dependents" on it. When this button is pressed,
the program will show a message box with a list of all the cells that use this cell's data. If there
was no formula in the cell, or the formula had no variables, the message box displays an appropriate message explaining why
there are no dependents for the selected cell.
- HelpPage: the help page references a rich text file relative to where the app is running. Since this file is found five levels above where
the app was running on our respective machines(next to the solution), if the app runs any lower or higher, the help page will not be found and will throw an unhandled error

## Assignment Specific Topics
Hours Estimate/Worked	|	Assignment	|	                     Note
------------------------|---------------|------------------------------
10	/	8	|	Assignment 1 - Formula Evaluator  |   Will have to spend more time rebuilding the code I wrote to meet the tests
8	/	6	|	Assignment 2 - Dependency Graph   |  Took me longer than it could have, I procrastinated a lot.
5	/	12	|	Assignment 3 - Formula			  |	This was much more complicated than I thought it would be.
10	/	16	|	Assignment 4 - Onward To a Spreadsheet|   I had to spend a lot of time rebuilding other classes to make this work.
12 /	7	|	Assignment 5 - Spreadsheet Model	|	This was so incredibly vague until it wasn't. Why did it take so long?
10	/	22	|	Assignment 06 - Spreadsheet Front-End Graphical User Interface	| The amount of time it took to understand the events related to every gui controller floored us and made this project much more difficult

##Testing Strategies
	Our testing strategies included manual testing of entering as many types of formulas that may throw exceptions to make sure that 
	the exceptions were thorwn were handled without killing the app. This strategy highlighted a critical failure of my Spreadsheet class. When the
	spreadsheet structure would throw a CircularException, the cell holding the faulty formula would not clear itself. When another cell would
	reference that faulty formula, they would pass back and forth between their look ups eventually overflowing the stack. While this required
	an undesireable edit in our spreadsheet class, the resulting behavior meant that a faulty formula could no longer be referenced.

	Additionally, every time a new feature or controller was added to the GUI, we would launch the app and use the controller. This would
	highlight any unhandled errors and would visually prove the concept. In some cases, this provided a type of regression testing. In addition
	to tesing the new controllers, we could make sure that they worked with old features and also that the old features could still work on 
	their own. Since we tested incrementally by feature, we were also able to spoof a type of focused unit testing by only allowing one feature
	to be affecting the GUI at a time.

## Consulted Peers
- Tyler Gordon
- Daniel Spyres

## References
- The C# Docs
- No other pages had relevant information relevant to the work I was doing

##Team Practices
We only merged to master with the other partner present to prevent the removal of essential code and to make sure
any added code didn't break what was there before. We were also really good at splitting the work so that we did not overlap
in responsibilities.

While we did a good job of splitting the work, we did not do such a fantastic job at making sure that eachother's code was
up to our personal standards. Additionally, there were some private helper methods that ended up slipping through the cracks 
that ended up being redundant while they did cause conflict. 

## Examples of Good Software Practice
Code Reuse - I was able to make a couple of private methods for simple repeated actions. Additionally, with the constructors
	I was able to use the base constructor to set the standards for the abstract spreadsheet instead of trying to do it over again
	in every instance of the constructor.
- Reression testing - I did not delete and was able to update my old tests to test my new code
- seperation of concerns - I was able to make a handful of private methods to handle small, repetitious actions.

