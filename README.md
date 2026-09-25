# WebApiHome3 — Модуль 03

## Цель работы

Закрепить навыки использования Dependency Injection, интерфейсов, регистрации сервисов и логирования ASP.NET Core.

## Краткий отчёт

Создано ASP.NET Core Web API для работы со студентами. Сервис `StudentService` передаётся в `StudentsController` через интерфейс `IStudentService` и Constructor Injection. Данные хранятся в `List<Student>` в памяти приложения. Для журнала применяется `ILogger<StudentsController>`.

## Модель Student

| Свойство | Тип | Назначение |
|---|---|---|
| `Id` | `int` | Идентификатор студента |
| `Name` | `string` | Имя студента |
| `Group` | `string` | Учебная группа |

## Ход выполнения

| Шаг | Выполненное действие | Результат |
|---|---|---|
| 1 | Создана модель `Student` | Добавлены `Id`, `Name`, `Group` |
| 2 | Созданы `IStudentService` и `StudentService` | Реализованы `GetAll()` и `GetById()`; добавлены 3 студента |
| 3 | Сервис зарегистрирован в `Program.cs` | Использован `AddTransient<IStudentService, StudentService>()` |
| 4 | Создан `StudentsController` | `IStudentService` передаётся через конструктор без `new` |
| 5 | Добавлен `ILogger<StudentsController>` | Использованы `LogInformation`, `LogWarning`, `LogError` |
| 6 | Выполнено тестирование Swagger | Получены ответы `200`, `404` и `400` |

## Реализованные endpoint

| Метод | Endpoint | Назначение | Результат |
|---|---|---|---|
| GET | `/api/students` | Получить всех студентов | `200 OK` |
| GET | `/api/students/{id}` | Получить студента по идентификатору | `200 OK`, `404 Not Found` или `400 Bad Request` |

## Пример JSON

```json
[
  {
    "id": 1,
    "name": "Aruzhan Saparova",
    "group": "SE-231"
  },
  {
    "id": 2,
    "name": "Daniyar Nurgaliyev",
    "group": "SE-231"
  }
]
```

## Проверка через Swagger

Через **Try it out → Execute** проверены:

| Запрос | Фактический результат |
|---|---|
| `GET /api/students` | `200 OK`, список из трёх студентов |
| `GET /api/students/1` | `200 OK`, студент Aruzhan Saparova |
| `GET /api/students/999` | `404 Not Found` |
| `GET /api/students/0` | `400 Bad Request`, демонстрация `LogError` |

## Логирование

В консоли приложения зафиксированы следующие сообщения:

```text
info: WebApiHome3.Controllers.StudentsController[0]
      Getting all students
warn: WebApiHome3.Controllers.StudentsController[0]
      Student with ID 999 was not found
fail: WebApiHome3.Controllers.StudentsController[0]
      Invalid student ID 0
```

## Жизненные циклы DI

| Жизненный цикл | Особенность | Пример |
|---|---|---|
| `Transient` | Новый экземпляр при каждом запросе зависимости | Лёгкий сервис без состояния |
| `Scoped` | Один экземпляр на HTTP-запрос | Сервис, работающий с контекстом БД |
| `Singleton` | Один экземпляр на всё время работы приложения | Кэш или потокобезопасный общий сервис |

В основной реализации выбран `Transient`, как требует задание. При замене на `AddSingleton<IStudentService, StudentService>()` сервис и его состояние становятся общими для всех запросов; поэтому реализация Singleton должна быть потокобезопасной.

## Ответы на контрольные вопросы

1. **Что такое Dependency Injection?** Передача зависимостей объекту извне, а не создание их внутри объекта.
2. **Зачем использовать интерфейс вместо `new`?** Контроллер зависит от контракта, поэтому реализацию легче заменить и протестировать.
3. **Что такое Constructor Injection?** Передача зависимостей через параметры конструктора.
4. **Для чего сервис регистрируется в `Program.cs`?** Это правило сообщает DI-контейнеру, какой объект создавать для интерфейса.
5. **Для чего используется `ILogger<T>`?** Для записи диагностических сообщений с категорией класса `T`.
6. **Чем отличаются LogInformation, LogWarning и LogError?** Information описывает нормальную работу, Warning — необычную ситуацию, Error — ошибку операции.

## Вывод

DI устраняет жёсткую связь `StudentsController` с конкретным сервисом, а `ILogger` делает выполнение API наблюдаемым. `Transient` создаёт новый сервис при каждом разрешении зависимости, тогда как `Singleton` хранит один общий экземпляр и требует внимательного отношения к состоянию и потокобезопасности.
