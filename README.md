# Тестовое задание для ZELF
Собственно само [задание](https://docs.google.com/document/d/1cQSs8dmYDCZT8HzZW7L7P-hsjrdAhGWM0qy174lkkZs/edit)

### Выбранные подходы
- Учитывая что задача имеет бизнес контекст (т.е не просто CRUD) то решил использовать богатую модель (DDD) вместо анемичной.
- Так как выбор пал на DDD, а значит доменная модель имеет только поведение, состояние инкапсулировано и для чтения данных нужна уже другая модель. 
Вследствии этого решил использовать CQRS, где будет использоваться отдельная модель для чтения. Ну и в принципе, команды из CQRS лучше отражают юз кейсы системы.
- В доменной модели есть доменные события. Необходимости их добавлять небыло, никто на них не подписан. Но причина по которой я добавил, это удобство тестирования, 
удобно проверять что результатом бизнес операции является доменное событие.
- ORM. Для работы с доменной моделью использовал EF Core, а модель для чтения используется с Dapper.

### Предметная область и модели
Итак, у клиента (Client) могут быть подписчики (ClientSubscriber). 
Также у клиента имеется популярность (ClientPopularity). Популярность клиента оценивается на основе кол-ва подписчиков (IPopularityEvaluator), так как больше у клиента ничего нет).

> PS. Я бы предпочел все выше изобразить в UML нежели описывать, но боюсь времени не хватит.

### Тесты
- Есть unit тесты
- Есть итеграционные тесты (в задании этого конечно не требовалось, но я не особый поклонник тестировать приложение в ручную, потому для решил добавить)

### Недочеты
- Хотелось бы добавить побольше тестов дабы покрыть больше юз кейсов
- Не успел добавить логгирование ;(
- Не успел добавить глобальный обработчик для исключений (ErrorController) где выполнялся бы mapping выброшенных исключений в соотвевующий ProblemDetails.
