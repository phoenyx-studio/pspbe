# Chronology of changes

This document contains changes of the project in reverse chronological order

## upcoming

Field Category in Post model references to Category object, however is displayed as CategoryId (integer). Need to change this behavior

Create and Update fields in both models are displayed as text. Need to format display.

Automatic fill fields Create and Update on save and (or) edit entities

Need to add authorization for both controllers

Develop front page

Wysiwyg-editor for post editing

## 2019-11-21

Models for Post and for Category changed - added Create and Update DateTime 

Added and executed migrations for updated models

Controller for Category replaced with new one

Controller for Post scaffolded

## 2019-11-06

Blog DB Context

Models for Post Category and Post

CRUD for Post categories

## 2019-11-03

Project is initialized with `dotnet new`. Then .gitignore is added. Now the project is able to run, but it uses mysqlite as its default database engine.

The project is set up to work with mysql 