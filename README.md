# MineSweeper
We often assign a coding project to applicants if they don't already have a portfolio of code. Sometimes as a developer it is easy to think that a problem is simple only to get lost in the details once the project begins. I want to make sure that I actually try the projects I ask others to do.

In this case I wanted to see if I could write a simple console-based mine sweeper game in an hour with an additional hour for unit testing.

## Requirements
* Console interface: Print board, get input
* Allow for various sizes of grids (width and height) and numbers of mines (but this can be hard coded)
* Take an x,y coordinate input.
* if the user hits a mine, end game with a loss.
* If there are adjacent mines show the number of adjacent mines in that cell.
* If a revealed cell is a 0, the reveal all adjacent cells recursively.
* Mark and unmark mines with *x,y
* Identify when the user has revealed all cells besides the mines and end game with a win.
* Unit testing on the Game class
 
## Development Notes
* [Initial game](https://github.com/GrantErickson/MineSweeper/commit/3f9511d47e414d0ab3975a743f536d0bfe83e4f9) took 1 hour.  
* [Unit testing](https://github.com/GrantErickson/MineSweeper/commit/8c072c50ba6986fc367a5f10692a91f2e4aa1996) took 1 hour.
