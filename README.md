# RptFileApp

This app parses an RPT file into a SQL server database in order to manage pendings accounts to collect.

## Table of Contents
1. [General Info](#general-info)
2. [Technologies](#technologies)
3. [Features](#features)
4. [How to Use](#how-to-use)
5. [Images](#images)
6. [Status](#status)
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
* jQuery

## Features:
* Upload a RPT formatted file, parse it and save all the information into a SQL server database.
* When being updated, compares the new file with the information already in the database to only upload new data.
* Removes all the information that is no longer present in the new file.
* Creates a new file with all the information that has been removed in the update.
* Creates a database row per each account which allows the user to inspect, edit and comment into each individual account or transaction.
* Has a metrics page that allows user to visualize the percentage owed by the biggest accounts in relationship with the total amount outstanding.



### To-do list:
* Improve UI.
* Use async methods to improve time efficiency.
* Display name of file after is clicked on when uploading it.

## How to Use:
1. In [FileLibrary/Data](https://github.com/joshuaconstante2197/RptFileApp/tree/master/FileProcessingLibrary/Data) you will find the SQL queries named *SQL SQLQuery1.sql* and *MoveAccountsDbQuery.sql*. Run those first.
2. In the same Data folder you will find the RPT files that PRISM (the CRM that the company this app was built for used) prints exactly as it does. They are named RPTFile1, 2 and 3.
3. Go to the **Upload new prism report** tab, click on upload and choose one of those files.
4. You will see all the information displayed in the home page. Click on **Delete zero and negative balance Accounts** after the first upload.
5. On the **Collections Files** tab you will find all previous files that have been uploaded and also the ones created to show the information that has been deleted after an  update. They are named with a GUID to avoid name collisions.
6. On the **Metrics** tab you will find a graph displaying the 10 biggest accounts in relationship to the total amount outstanding.
7. In the home screen you can select any individual account, quickly add comments to it since it will show only the latest comment, or go to **Details** wich will show you more information about that account, will allow you to edit information about the account and will let you see any previous comments.

## Images:
1. Home Page ![alt text](https://github.com/joshuaconstante2197/RptFileApp/blob/master/FileProcessingLibrary/Data/img/all-accounts.PNG)
2. Account Details ![alt text](https://github.com/joshuaconstante2197/RptFileApp/blob/master/FileProcessingLibrary/Data/img/account-details.PNG)
3. Previous Files ![alt text](https://github.com/joshuaconstante2197/RptFileApp/blob/master/FileProcessingLibrary/Data/img/collections-files.PNG)
4. Metrics Page ![alt text](https://github.com/joshuaconstante2197/RptFileApp/blob/master/FileProcessingLibrary/Data/img/collections-app%20project.png)

## Status:

Project is: *in progress*




Created by [@joshua.co.dev](https://www.https://portfolio-website-4l9ay.ondigitalocean.app/projects/portfolio-item-piano.html.pl/) 
