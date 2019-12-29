# Chronology of changes

This document contains changes of the project in reverse chronological order

## upcoming

Need to add authorization for both controllers

Use bootstrap layout for Post and Category controllers

Init controller for the situation when there is no root at the system

Develop front page

Wysiwyg-editor for post editing

## 2019-12-22

Update and Create fields of both Post and Category are automaticaly filled with DateTime.Now on creation of the entity. On edit field Create remain the same, Update is filled with DateTime.Now

## 2019-12-21

Applied display format for Create and Update fields in both models.

## 2019-11-27

Change CategoryId to CategoryTitle on pages of PostController

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