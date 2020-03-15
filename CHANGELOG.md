# Chronology of changes

This document contains changes of the project in reverse chronological order

## upcoming

Document process of the application initial deployment

Form input controls are too narrow #12

Develop front page

Wysiwyg-editor for post editing

## 2020-03-15

Make views be able to render HTML in post content #13

## 2020-01-05

Remove post content from the view of the list of the posts #14

## 2020-01-04

Change input type for post content from text to textarea on create and edit pages

## 2019-12-30

Policies to check if the user is root or admin (issue #6)

Use bootstrap layout for Post and Category controllers (issue #10)

## 2019-12-29

Added authorization for Post and Category controllers - allowed only for admins. Access to Role and User controllers is allowed only for users in the role of root.

Added Init controller for the situations when the application is deployed with empty database. Resolves issue #7

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