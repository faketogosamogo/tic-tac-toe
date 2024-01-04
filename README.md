Реализовать сервис игры в крестики нолики HTTP WebApi на .Net Core 6.0 и выше.
1) Метод для создания игры - должен вернуть идентификатор игры в виде токена, и карту игры. Также предусмотреть выбор очередности хода(крестик или нолик). Если первым ходит бот, то его ход уже должен быть выполнен.
2) Метод получения актуального состояния игры (если игрок закроет фронт и придет доиграть позже), клиент передает токен для получения состояния.
3) Метод выполнения хода. Клиент отправляет запрос на сервер о ходе игрока, сервис обрабатывает запрос и возвращает ответ (бот делает ход и/либо игра завершается).
   Будет плюсом реализация минимального простейшего фронта для тестирования, оценивается прежде всего бэк. В качестве базы для хранения состояния рекомендуется использовать MongoDB.
   Исходный код залить на GitHub.

Путь к бэкенду прописан в TicTacToe/src/TicTacToe.Spa/src/app/game.service.ts