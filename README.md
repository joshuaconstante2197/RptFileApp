# RptFileApp

This app parses an RPT file into a SQL server database in order to manage pendings accounts to collect.

## Table of Contents
1. [General Info](#general-info)
2. [Technologies](#technologies)
3. [Features](#features)
4. [How to Use](#how-to-use)
## General Info:

This app was created for a company I was working with. They used a system called PRISM as their CRM which printed out RPT formatted files in order to manage collections.
Before I created the app it was necessary to print a report, clean it (since it contained a lot of unnecessary and often confusing information), 
format it into a comprehensible excel file, add information from previous weeks and then use it. This was done at least once a week, took several 
hours and made it near impossible to stay up to date.

This app solved various problems in the collections process.

* **Time efficiency**
* **Data persistency and consistency**
* **Insights and management**

## Technologies

Project is created with:
* ASP.NET
* Razor Pages
* SQL Server
* JavaScript
* HTML
* CSS

## Features:
* Upload a RPT file, parse it and save all the information into a SQL server database.
* When being updated, compares the new file with the information already in the database to only upload new data.
* Removes all the information that is no longer present in the new file.
* Creates a new file with all the information that has been removed in the update.
* Creates a database row per each account which allows the user to inspect, edit and comment into each individual account.
* Has a metrics page that allows user to visualize the percentage owed by the biggest accounts in relationship with the total amount outstanding.

### To-do list:
* Improve UI.
* Use async methods to improve time efficiency.

## How to Use:
1. Download

## Status:

Project is: *in progress*




Created by [@joshua.co.dev](https://www.https://portfolio-website-4l9ay.ondigitalocean.app/projects/portfolio-item-piano.html.pl/) 
