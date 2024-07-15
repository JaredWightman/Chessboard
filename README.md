# Chessboard Project

## Project Purpose and Overview
The purpose behind this project is to learn teamwork and development concepts surrounding game development with Unity, collaboration using version control (specifically Git/Github), and increasing our knowledge of C#. We've designed and built a chess game that presents a main menu to the user, and allows them to begin a new turn-based game. We prevent illegal moves, highlight available moves, and recognize when an opposing team's piece is captured. 

To play this game, load the project in Unity version 2022.3.28f1

## Code Overview
### [GameManager.cs](Assets/Scripts/GameManager.cs) 
- Handles piece selection and placement across the board 
  using multiple methods that receive input from the 
  mouse position.

### [mainMenu.cs](Assets/mainMenu.cs)
- Loads the FinalProject (bonus game), Chessexperiments 
  (normal game), and Menu in three separate class methods.

### [Board.cs](Assets/Scripts/Board.cs)
- Creates the board, creates the pieces for game, places the pieces, and defines movements for each piece.

### [Coord.cs](Assets/Scripts/Coord.cs)
- Creates coordination system for the program to use to better navigate the board.

### [Piece.cs](Assets/Scripts/Piece.cs)
- Creates attributes for the pieces including type and colors.

### [Square.cs](Assets/Scripts/Square.cs)
- Creates individual square with certain attributes and then defines Get and Set Methods for the square attributes.

### [BoardUI.cs](Assets/Scripts/UI/BoardUI.cs)
- Controls various elements of the UI during gameplay, allowing for customization over whether the board flips to maintain correct perspective for the player whose current turn it is, 

### [BoardTheme.cs](Assets/Scripts/UI/BoardTheme.cs)
- Holds information about board UI elements such as square colors under different selection states, or the default color. 

### [PieceTheme.cs](Assets/Scripts/UI/PieceTheme.cs)
- Allows for easy customization and retrieval of piece sprites based on piece color, supporting both black and white themes.

[Software Demo Video](http://youtube.link.goes.here)


## Development Environment

This project was developed with Unity and CSharp on VSCode.

## Collaborators
- Jared Wightman
- Ethan Leishman
- Spencer Ashcraft
- Nathan Brower
- Luke Wells

## Useful Websites and Works Cited

- [Chat GPT](https://chatgpt.com/)
- [Chess Piece Sprites - cburnett](https://commons.wikimedia.org/wiki/Category:PNG_chess_pieces/Standard_transparent)
- [Chess Piece Sprites - Merida](https://www.zoomchess.com/images/sets/merida/)
- [Board Sprite](https://en.m.wikipedia.org/wiki/File:Chessboard480.svg)
- [Coding Adventure: Chess](https://www.youtube.com/watch?v=U4ogK0MIzqk&ab_channel=SebastianLague)
- [How to use GitHub with Unity](https://www.youtube.com/watch?v=qpXxcvS-g3g&ab_channel=Brackeys)
- [Create a grid in Unity](https://www.youtube.com/watch?v=kkAjpQAM-jE&ab_channel=Tarodev)

## Future Work
- Pawn promotion to queen/knight/bishop/rook needs to be fully built.
- Both check and checkmate conditions need to result in an end of game action. Currently only checkmate works.