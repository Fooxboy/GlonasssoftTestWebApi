# Тестовое задание для ГЛОНАССсофт

[![.NET](https://github.com/Fooxboy/GlonasssoftTestWebApi/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/Fooxboy/GlonasssoftTestWebApi/actions/workflows/dotnet.yml)

### Методы api:

**POST:** ``/report/user_statistics`` - *возвращает guid запроса для получения отчета пользователя*

**Аргументы в  теле запроса:**

``Guid userId`` - *Идентификатор пользователя, для которого нужно получить отчет.*

``DateTime fromDate`` - *Период с*

``DateTime toDate`` - *Период по*

------------


**GET:** ``/report/info`` - *возвращает информацию об отчете или о состоянии его готовности*

**Аргументы:**

``Guid requestId`` - *Идентификатор запроса*

**Возвращает объект** ``ReportModel``


------------


### Возвращаемые объекты:

**ReportModel**  - *модель содержащая информацию о отчете или его состоянии готовности.*

**Содержит поля:**

``string query`` - Идентификатор запроса

``int percent`` - Процент готовности отчета

``ReportResultModel result`` - результат отчета (null, если percent не 100%)

------------


**ReportResultModel** - *модель содержащая информацию об отчете*

**Содержит поля:**

``string user_id`` - Идентификатор пользоваетеля, которому принадлежит отчет

``int count_sign_in`` - Количество входов пользователя









