using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using noughts_and_crosses.Services;
using NUnit.Framework;

namespace noughts_and_crosses_tests
{
    public class TicTacToeTests
    {
        private ITicTacToeService _ticTacToeService;
        private ITicTacToeRandomService _ticTacToeRandomService;

        private const int _fixedNumberOfRowsAndColumns = 3;
        
        [SetUp]
        public void Setup()
        {
            _ticTacToeService = new TicTacToeService();
            _ticTacToeRandomService = new TicTacToeRandomService(_ticTacToeService);
        }

        [TestCase(new int[] { 88, 88, 88, 4, 5, 6, 7, 8, 9 })] // Player 1 'X' Wins first row
        [TestCase(new int[] { 1, 2, 3, 88, 88, 88, 7, 8, 9 })] // Player 1 'X' Wins second row
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 88, 88, 88 })] // Player 1 'X' Wins third row
        [TestCase(new int[] { 79, 79, 79, 4, 5, 6, 7, 8, 9 })] // Player 2 'O' Wins first row
        [TestCase(new int[] { 1, 2, 3, 79, 79, 79, 7, 8, 9 })] // Player 2 'O' Wins second row
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 79, 79, 79 })] // Player 2 'O' Wins third row
        public void AllSameCheckedRows_ShouldReturn_WinCondition(int[] rowWinningNumbers)
        {
            //Arrange
            var expectedBoardState = 1; // Win Condition

            //* ASCII 79 ==> 'O', ASCII 88 ==> 'X'

            //Player 1: X and Player 2: O
            //Base case 3 x 3

            // ---------------------------------------------
            //                  TEST CASE 1

            // 1 2 3      ====>     X X X     ====>     Win
            // 4 5 6                - - -
            // 7 8 9                - - -

            // ---------------------------------------------
            //                  TEST CASE 2

            // 1 2 3                - - -     
            // 4 5 6      ====>     X X X     ====>     Win
            // 7 8 9                - - -

            // ---------------------------------------------
            //                  TEST CASE 3

            // 1 2 3                - - -     
            // 4 5 6      ====>     - - -     
            // 7 8 9                X X X     ====>     Win

            // ---------------------------------------------
            //                  TEST CASE 4

            // 1 2 3      ====>     O O O     ====>     Win
            // 4 5 6                - - -
            // 7 8 9                - - -

            // ---------------------------------------------
            //                  TEST CASE 5

            // 1 2 3                - - -     
            // 4 5 6      ====>     O O O     ====>     Win
            // 7 8 9                - - -

            // ---------------------------------------------
            //                  TEST CASE 6

            // 1 2 3                - - -     
            // 4 5 6      ====>     - - -     
            // 7 8 9                O O O     ====>     Win

            // ---------------------------------------------
            var boardState = _ticTacToeService.CheckTicTacToeBoardState(rowWinningNumbers, _fixedNumberOfRowsAndColumns);

            //Assert
            Assert.AreEqual(expectedBoardState, boardState);
        }

        [TestCase(new int[] { 88, 2, 3, 88, 5, 6, 88, 8, 9 })] // Player 1 'X' Wins first column
        [TestCase(new int[] { 1, 88, 3, 4, 88, 6, 7, 88, 9 })] // Player 1 'X' Wins second column
        [TestCase(new int[] { 1, 2, 88, 4, 5, 88, 7, 8, 88 })] // Player 1 'X' Wins third column
        [TestCase(new int[] { 79, 2, 3, 79, 5, 6, 79, 8, 9 })] // Player 2 'O' Wins first column
        [TestCase(new int[] { 1, 79, 3, 4, 79, 6, 7, 79, 9 })] // Player 2 'O' Wins second column
        [TestCase(new int[] { 1, 2, 79, 4, 5, 79, 7, 8, 79 })] // Player 2 'O' Wins third row
        public void AllSameCheckedColumns_ShouldReturn_WinCondition(int[] columnWinningNumbers)
        {
            //Arrange
            var expectedBoardState = 1; // Win Condition

            //Act

            //* ASCII 79 ==> 'O', ASCII 88 ==> 'X'

            //Base case 3 x 3

            // ---------------------------------------------
            //                  TEST CASE 1

            // 1 2 3      ====>     X - -     ====>     Win
            // 4 5 6                X - -      
            // 7 8 9                X - -

            // ---------------------------------------------
            //                  TEST CASE 2

            // 1 2 3                - X -     
            // 4 5 6      ====>     - X -     ====>     Win
            // 7 8 9                - X -

            // ---------------------------------------------
            //                  TEST CASE 3

            // 1 2 3                - - X     
            // 4 5 6      ====>     - - X     
            // 7 8 9                - - X     ====>     Win

            // ---------------------------------------------
            //                  TEST CASE 4

            // 1 2 3      ====>     O - -     ====>     Win
            // 4 5 6                O - -      
            // 7 8 9                O - -

            // ---------------------------------------------
            //                  TEST CASE 5

            // 1 2 3                - O -     
            // 4 5 6      ====>     - O -     ====>     Win
            // 7 8 9                - O -

            // ---------------------------------------------
            //                  TEST CASE 6

            // 1 2 3                - - O     
            // 4 5 6      ====>     - - O     
            // 7 8 9                - - O     ====>     Win

            // ---------------------------------------------

            var boardState = _ticTacToeService.CheckTicTacToeBoardState(columnWinningNumbers, _fixedNumberOfRowsAndColumns);

            //Assert
            Assert.AreEqual(expectedBoardState, boardState);
            
        }

        [TestCase(new int[] { 88, 2, 3, 4, 88, 6, 7, 8, 88 })] // Player 1 'X' Wins first diagonal
        [TestCase(new int[] { 1, 2, 88, 4, 88, 6, 88, 8, 9 })] // Player 1 'X' Wins second diagonal
        [TestCase(new int[] { 79, 2, 3, 4, 79, 6, 7, 8, 79 })] // Player 2 'O' Wins first diagonal
        [TestCase(new int[] { 1, 2, 79, 4, 79, 6, 79, 8, 9 })] // Player 2 'O' Wins second diagonal

        public void AllSameCheckedDiagonals_ShouldReturn_WinCondition(int[] diagonalWinningNumbers)
        {
            //Arrange
            var expectedBoardState = 1; // Win Condition

            //Act

            //* ASCII 79 ==> 'O', ASCII 88 ==> 'X'

            //Base case 3 x 3

            // ---------------------------------------------
            //                  TEST CASE 1

            // 1 2 3      ====>     X - -     ====>     Win
            // 4 5 6                - X -      
            // 7 8 9                - - X

            // ---------------------------------------------
            //                  TEST CASE 2

            // 1 2 3                - - X     
            // 4 5 6      ====>     - X -     ====>     Win
            // 7 8 9                X - -

            // ---------------------------------------------
            //                  TEST CASE 3

            // 1 2 3      ====>     O - -     ====>     Win
            // 4 5 6                - O -      
            // 7 8 9                - - O

            // ---------------------------------------------
            //                  TEST CASE 4

            // 1 2 3                - - O     
            // 4 5 6      ====>     - O -     ====>     Win
            // 7 8 9                O - -
            // ---------------------------------------------

            var boardState = _ticTacToeService.CheckTicTacToeBoardState(diagonalWinningNumbers, _fixedNumberOfRowsAndColumns);

            //Assert
            Assert.AreEqual(expectedBoardState, boardState);

        }

        [TestCase(new int[] {88, 88, 79, 79, 79, 88, 88, 79, 88})] // Draw
        public void AllCheckedPositions_NoWinCondition_ShouldReturn_DrawCondition(int[] drawConditionNumbers)
        {
            //Arrange

            var expectedBoardState = 0; // Draw Condition / All input ('X' or 'O') checked positions
            var expectedAllCheckedPositions = true;

            //Act

            //* ASCII 79 ==> 'O', ASCII 88 ==> 'X'

            //Base case 3 x 3

            // ---------------------------------------------
            //                  TEST CASE 1

            // 1 2 3      ====>     X X O     ====>     Draw
            // 4 5 6                O O X      
            // 7 8 9                X O X

            // ---------------------------------------------

            var boardState =
                _ticTacToeService.CheckTicTacToeBoardState(drawConditionNumbers, _fixedNumberOfRowsAndColumns);

            var allCheckedPositions = _ticTacToeService.CheckAllTicTacToePositionsChecked(drawConditionNumbers);

            //Assert

            Assert.AreEqual(expectedBoardState, boardState);
            Assert.AreEqual(expectedAllCheckedPositions, allCheckedPositions);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 88, 88, 88}, 88)] // Player 1 Wins
        [TestCase(new int[] {1, 2, 79, 4, 79, 6, 79, 8, 9}, 79)] // Player 2 Wins
        public void WinCondition_ShouldReturn_CorrectWinningPlayer(int[] winningNumbers, int player)
        {
            //Arrange
            var winningPlayer = player;
            var ticTacToeArr = new int[_fixedNumberOfRowsAndColumns, _fixedNumberOfRowsAndColumns];
            
            //Act
            var ticTacToeDataStructure =
                _ticTacToeService.ProcessTicTacToeLines(ticTacToeArr, winningNumbers, _fixedNumberOfRowsAndColumns);
            
            var winningLine = _ticTacToeService.CheckTicTacToeWinningLine(ticTacToeDataStructure);

            //88 is Player 1 ------ 79 is Player 2
            var winner = winningLine.Item1.FirstOrDefault();
            var winnerLine = winningLine.Item1.Aggregate(new StringBuilder(),
                    (current, next) => current.Append(current.Length == 0 ? "" : ", ").Append(next))
                .ToString();
            var positionNumber = winningLine.Item2.Item1;
            var position = winningLine.Item2.Item2;

            Debug.WriteLine($"The winning line is {winnerLine} at {position} {positionNumber}");
            //Assert
            Assert.AreEqual(winningPlayer, winner);

        }

        [TestCase(new int[] { 1, 2, 79, 4, 79, 6, 79, 8, 9 }, 88)] // Player 1 Loses
        [TestCase(new int[] { 88, 2, 3, 4, 88, 6, 7, 8, 88 }, 79)] // Player 2 Loses
        public void LoseCondition_ShouldReturn_CorrectLosingPlayer(int[] losingNumbers, int player)
        {
            //Arrange
            var losingPlayer = player;
            var ticTacToeArr = new int[_fixedNumberOfRowsAndColumns, _fixedNumberOfRowsAndColumns];

            //Act
            var ticTacToeDataStructure =
                _ticTacToeService.ProcessTicTacToeLines(ticTacToeArr, losingNumbers, _fixedNumberOfRowsAndColumns);

            var winningLine = _ticTacToeService.CheckTicTacToeWinningLine(ticTacToeDataStructure);

            //88 is Player 1 ------ 79 is Player 2
            var winner = winningLine.Item1.FirstOrDefault();
            var winnerLine = winningLine.Item1.Aggregate(new StringBuilder(),
                    (current, next) => current.Append(current.Length == 0 ? "" : ", ").Append(next))
                .ToString();
            var positionNumber = winningLine.Item2.Item1;
            var position = winningLine.Item2.Item2;

            Debug.WriteLine($"The winning line is {winnerLine} at {position} {positionNumber}");
            //Assert
            Assert.AreNotEqual(losingPlayer, winner);

        }

        [TestCase(new int[] {79, 2, 79, 4, 79, 6, 79, 88, 79})]
        public void GenerateNextPossibleMove_ShouldReturn_RandomMove(int[] board)
        {
            //Arrange
            var expectedValue = 0;
            //Act
            var randomMove = _ticTacToeRandomService.GenerateNextPossibleMove(board);
            //Assert
            Assert.AreNotEqual(expectedValue, randomMove);
        }

        [TestCase(new int[] { 79, 79, 88, 79, 88, 79, 88, 79, 88 })]
        public void GenerateNextPossibleMove_OnAllCheckedBoard_ShouldReturnZero(int[] board)
        {
            //Arrange
            var expectedValue = 0;
            //Act
            var randomMove = _ticTacToeRandomService.GenerateNextPossibleMove(board);
            //Assert
            Assert.AreEqual(expectedValue, randomMove);
        }

        //[TestCase(new int[] { 88, 88, 3, 4, 5, 6, 7, 8, 9 }, false, 3)]
        //[TestCase(new int[] { 79, 79, 3, 4, 5, 6, 7, 8, 9 }, false, 3)]
        [TestCase(new int[] { 88, 79, 88, 79, 88, 6, 7, 8, 9 }, true, 7)]
        public void GenerateNextBestPossibleMove_ShouldReturnBestPossibleMove(int[] board, bool isDiagonal, int nextPossibleMove)
        {
            //Arrange
            var expectedValue = nextPossibleMove;
            //Act
            var randomMove = _ticTacToeRandomService.GenerateNextBestPossibleMove(board, _fixedNumberOfRowsAndColumns);
            //Assert
            Assert.AreEqual(expectedValue, randomMove);
        }

    }
}