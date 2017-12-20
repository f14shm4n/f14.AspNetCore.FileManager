# Json format

For the data exchange I used json format.

The format is separated on the request data and response data:

[Requests](#requests)

[Responses](#responses)

## Requests

[Copy param](#copy-param)
  
[Move param](#move-param)

[Create folder param](#create-folder-param)

[Delete param](#delete-param)

[Folder struct param](#folder-struct-param)

[Rename param](#rename-param)
  
### Copy param

This is the json format for the copy request.

```
{
  "SourceDirectory": "/", // The path to the directory from which you want to copy the data.
  "DestinationDirectory": "/", // The path to the directory into which you want to insert data.
  "Overwrite": false, // Determines whether to overwrite files.
  "Targets": [ // The collection of the targets which you want to copy.
    {
      "Name": "sample.txt", // The name of the file or folder.
      "IsFile": true // Determines whether object is file or folder.
    }
  ],
  "CurrentFolderPath": "/" // The path to the current directory, where is the user located.
}
```

### Move param

This is the json format for the move request.

```
{
  "SourceDirectory": "/", 
  "DestinationDirectory": "/",
  "Overwrite": false, 
  "Targets": [ 
    {
      "Name": "sample.txt",
      "IsFile": true 
    }
  ],
  "CurrentFolderPath": "/"
}
```

### Create folder param

This is the json format for the create folder request.

```
{
  "Name": "new folder name", // The name of the new folder.
  "CurrentFolderPath": "/"
}
```

### Delete param

This is the json format for the delete request.

```
{
  "Targets": [ // The collection of the targets which you want to delete.
    "itemNameToDelete" // The name of the deleting object.
  ],
  "CurrentFolderPath": "/"
}
```

### Folder struct param

This is the json format for the folder struct request.

```
{
  "FileExtensions": [ // The collection of the file extensions which you want to receive.
    ".txt" // The file extension.
  ],
  "CurrentFolderPath": "/"
}
```

### Rename param

This is the json format for the rename request.

```
{
  "Targets": [ // The collection of the objects which you want to rename.
    {
      "OldName": "old name", // The old object name.
      "Name": "new name", // The new object name.
      "IsFile": true
    }
  ],
  "CurrentFolderPath": "/"
}
```
  
  
## Responses

[Copy result](#copy-result)
  
[Move result](#move-result)

[Create folder result](#create-folder-result)

[Delete result](#delete-result)

[Folder struct result](#folder-struct-result)

[Rename result](#rename-result)

### Copy result

This is the json format for the copy response.

```
{
  "Errors": [ // The collection of the errors which can happen while processing the request.
    "Error message." // The specified error message.
  ]
}
```

### Move result

This is the json format for the move response.

```
{
  "Errors": [
    "Error message." 
  ]
}
```

### Create folder result

This is the json format for the create folder response.

```
{
  "Errors": [ 
    "Error message."
  ]
}
```

### Delete result

This is the json format for the delete response.

```
{
  "Affected": 0, // The count of affected objects.
  "Errors": [
    "Error message."
  ]
}
```

### Folder struct result

This is the json format for the folder struct response.

```
{
  "FolderCount": 1, // The count of returned folders.
  "FileCount": 1, // The count of returned files.
  "Folders": [ // The folder collections.
    {
      "Name": "folder name", // Folder name.
      "Properties": { // Object properties collection.
        "Property": "Value" // Property key-value.
      }
    }
  ],
  "Files": [
    {
      "Name": "file.name",
      "Properties": {
        "Property": "Value"
      }
    }
  ],
  "Errors": [
    "Error message."
  ]
}
```

### Rename result

This is the json format for the rename response.

```
{
  "RenamedObjects": [ // The objects that have been renamed.
    {
      "OldName": "old name", // Old name
      "Name": "new name", // New name (current file or folder name)
      "IsFile": true // File or folder flag
    }
  ],
  "Affected": 0,
  "Errors": [
    "Error message."
  ]
}
```
