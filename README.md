# Chessboard Project

## Project Purpose and Overview
The purpose behind this project is to learn teamwork and development concepts surrounding game development with Unity, collaboration using version control (specifically Git/Github), and increasing our knowledge of C#. We've designed and built a chess game that presents a main menu to the user, and allows them to begin a new turn-based game. We prevent illegal moves, highlight available moves, and recognize when an opposing team's piece is captured. 

## Code Overview
### GameManager.cs 
- handles piece selection and placement

### mainMenu.cs

### Board.cs

### Coord.cs

### Piece.cs

### Square.cs

### BoardUI.cs
- Controls various elements of the UI during gameplay, allowing for customization over whether the board flips to maintain correct perspective for the player whose current turn it is, 

### BoardTheme.cs
- Holds information about board UI elements such as square colors under different selection states, or the default color. 

### PieceTheme.cs
- Allows for easy customization and retrieval of piece sprites based on piece color, supporting both black and white themes.

## Development Environment

This project was developed with Unity and CSharp on VSCode.

## Useful Websites and Works Cited

- [Chat GPT](https://chatgpt.com/)
- [Chess Piece Sprites](https://commons.wikimedia.org/wiki/Category:PNG_chess_pieces/Standard_transparent)
- [Board Sprite](https://en.m.wikipedia.org/wiki/File:Chessboard480.svg)
- [Coding Adventure: Chess](https://www.youtube.com/watch?v=U4ogK0MIzqk&ab_channel=SebastianLague)
- [How to use GitHub with Unity](https://www.youtube.com/watch?v=qpXxcvS-g3g&ab_channel=Brackeys)
- [Create a grid in Unity - Perfect for tactics or turn-based games!](https://www.youtube.com/watch?v=kkAjpQAM-jE&ab_channel=Tarodev)


## Future Work
- We discussed modifying the board to allow to expansion beyond the normal bounds of a chessboard in order to allow for gameplay changes such as more than two teams, or 3D chess. This is not implemented currently. 
- Pawn promotion to queen/knight/bishop/rook needs to be fully built.
- Check and Checkmate conditions need to result in an end of game action.

## Remaining Issues
