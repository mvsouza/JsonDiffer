# Json Differ Project

## Archtecture overview

First of all I've "scratched" a package design solution to deliver the described assignment. Drawing is a good way to communicate my intent and clear some design assumptions. I would probably use a piece of paper or a whiteboard, but for later review I've put some extra effort to do it on a Plantuml. 

![Architecture](docs/diagrams/package.svg)

The architecture/design has MVC, CQS, DDD and event driven patterns. Also, used TDD, BDD, code clean practices.

On the VCS I've implemented a CI pipeline that builds the solution, run the unit and integration and code analyses, then, if every thing passes, it pushes a container to the staging environment.

## To-do-List

### Product managment and release

- [x] Problem analysis
- [x] Archtecture concept
- [x] Archtecture overview
- [x] CI pipeline
- [x] [Backlog(Userstories)](docs/Backlog.md)
- [ ] Writte down Bdd Scenarios
  - [ ] Post JSON doc for Diff
    - [x] Post valid json sides for right and left side
    - [x] Post invalid json for right and left side
    - [ ] Post duplicated json for right and left side
  - [ ] Post request for JSON diff
    - [x] Rest result for equal jsons
    - [x] Rest result for different sizes jsons
    - [x] Rest result for equal sizes and different jsons
    - [ ] Inform if the key wasn't posted 
    - [ ] Inform if any side wasn't filled 

### Technical Tasks

- [x] Project setup
- [ ] Remove fix messages from code
- [ ] Setup a database for persistence 
- [ ] Find better response message format for identifying diff with different sizes
- [ ] Create scripts to easyly do exploratory test(swagger-codegen)

## Pipeline metrics

### Master

[![Build Status](https://travis-ci.org/mvsouza/JsonDiffer.svg?branch=master)](https://travis-ci.org/mvsouza/JsonDiffer)[![Build status](https://ci.appveyor.com/api/projects/status/gpgef02rfvdqrwhs/branch/master?svg=true)](https://ci.appveyor.com/project/mvsouza/JsonDiffer/branch/master)[![codecov](https://codecov.io/gh/mvsouza/JsonDiffer/branch/master/graph/badge.svg)](https://codecov.io/gh/mvsouza/JsonDiffer)

### Develop

[![Build Status](https://travis-ci.org/mvsouza/JsonDiffer.svg?branch=develop)](https://travis-ci.org/mvsouza/JsonDiffer)[![Build status](https://ci.appveyor.com/api/projects/status/gpgef02rfvdqrwhs/branch/develop?svg=true)](https://ci.appveyor.com/project/mvsouza/JsonDiffer/branch/develop)[![codecov](https://codecov.io/gh/mvsouza/JsonDiffer/branch/develop/graph/badge.svg)](https://codecov.io/gh/mvsouza/JsonDiffer)

### SonarCloud

[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=JsonDiffer&metric=alert_status)](https://sonarcloud.io/dashboard?id=JsonDiffer) [![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=JsonDiffer&metric=coverage)](https://sonarcloud.io/component_measures?id=JsonDiffer&metric=coverage) [![SonarCloud Bugs](https://sonarcloud.io/api/project_badges/measure?project=JsonDiffer&metric=bugs)](https://sonarcloud.io/project/issues?id=JsonDiffer&resolved=false&types=BUG) [![SonarCloud Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=JsonDiffer&metric=vulnerabilities)](https://sonarcloud.io/project/issues?id=JsonDiffer&resolved=false&types=VULNERABILITY)
[![SonarCloud Codesmels](https://sonarcloud.io/api/project_badges/measure?project=JsonDiffer&metric=code_smells)](https://sonarcloud.io/project/issues?id=JsonDiffer&resolved=false&types=code_smells)