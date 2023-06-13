# Digipolis Swagger library

## 1.0.7

- Added lock when creating the directory and file to assure thread safety

## 1.0.6

- set correct path to include xml-comments

## 1.0.5

- Remove version from Paths in swaggerDoc to comply with openapi-validator.
- Set api version in OpenApiServer Url for OpenApiDocument. Serialize as V2 will set basePath to version on swaggerDoc, removing need for version in Paths.
- Make download button on swagger ui optional.

## 1.0.4

- Set swagger definition to Swagger v2 in stead of OpenApi v3, openapi-validator.antwerpen.be only supports up to v2

## 1.0.2 - 1.0.3

- Added code to create the AddPublishJsonButton.js file on startup from embedded resource
- Updated unit tests

## 1.0.1

- Update code based on PR feedback.
- Added Download publish JSON button to the Swagger UI which can be used for 
  downloading the Swagger JSON without versioning.

## 1.0.0

- initial version.

