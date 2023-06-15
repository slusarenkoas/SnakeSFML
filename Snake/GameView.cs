﻿using SFML.Graphics;
using SFML.System;

namespace Snake;

/// <summary>
/// Класс отвечает за отрисовку игры.
/// </summary>
public class GameView
{
    // Настройки отрисовки игры.
    private GameViewSettings _gameViewSettings;

    // Змейка.
    private Snake _snake;

    // Игровое поле.
    private GameBoard _gameBoard;

    // Контроллер еды.
    private FoodController _foodController;

    // Спрайты объектов
    private Sprite _wallSprite;
    private Sprite _backgroundSprite;
    private Sprite _snakeBodySprite;
    private Sprite _snakeHeadSprite;
    private Sprite _foodSprite;


    public GameView(GameViewSettings gameViewSettings, GameBoard gameBoard, Snake snake, FoodController foodController)
    {
        _gameViewSettings = gameViewSettings;
        _snake = snake;
        _foodController = foodController;
        _gameBoard = gameBoard;


        _wallSprite = new Sprite(_gameViewSettings.WallTexture);
        _backgroundSprite = new Sprite(_gameViewSettings.BackgroundTexture);
        _foodSprite = new Sprite(_gameViewSettings.FoodTexture);
        _snakeBodySprite = new Sprite(_gameViewSettings.SnakeBodyTexture);
        _snakeHeadSprite = new Sprite(_gameViewSettings.SnakeHeadTexture);
    }

    /// <summary>
    /// Метод рисует все игровые объекты.
    /// </summary>
    public void DrawGameObjects(RenderWindow window)
    {
        DrawFood(window);
        DrawSnake(window);
    }

    /// <summary>
    /// Метод рисует карту.
    /// </summary>
    public void DrawMap(RenderWindow window)
    {
        window.Draw(_backgroundSprite);
        for (int i = 0; i < _gameBoard.Size.X; i++)
        {
            _wallSprite.Position = new Vector2f(i * _gameViewSettings.CellSize, 0);
            window.Draw(_wallSprite);
            _wallSprite.Position = new Vector2f(i * _gameViewSettings.CellSize, (_gameBoard.Size.Y - 1) * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);
        }
        
        for (int i = 0; i < _gameBoard.Size.Y; i++)
        {
            _wallSprite.Position = new Vector2f(0, i * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);
            _wallSprite.Position = new Vector2f((_gameBoard.Size.Y - 1) * _gameViewSettings.CellSize,i * _gameViewSettings.CellSize);
            window.Draw(_wallSprite);
        }
    }

    /// <summary>
    /// Метод рисует еду на карте
    /// </summary>
    private void DrawFood(RenderWindow window)
    {
        // Умножаем позицию еды, на размер ячейки
        _foodSprite.Position = new Vector2f(_foodController.Food.X * _gameViewSettings.CellSize,
            _foodController.Food.Y * _gameViewSettings.CellSize);
        window.Draw(_foodSprite);
    }

    /// <summary>
    /// Метод рисует змейку на карте
    /// </summary>
    private void DrawSnake(RenderWindow window)
    {
        for (int i = 0; i < _snake.GetSize(); i++)
        {
            if (i == 0)
            {
                _snakeHeadSprite.Position = new Vector2f(
                    _snake.GetPoint(i).X * _gameViewSettings.CellSize,
                    _snake.GetPoint(i).Y * _gameViewSettings.CellSize);
                window.Draw(_snakeHeadSprite);
            }
            else
            {
                _snakeBodySprite.Position = new Vector2f(
                    _snake.GetPoint(i).X * _gameViewSettings.CellSize,
                    _snake.GetPoint(i).Y * _gameViewSettings.CellSize);
                window.Draw(_snakeBodySprite);
            }
        }
    }
}