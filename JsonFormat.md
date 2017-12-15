# Json format

For the data exchange I used json format.

Request:

  [Copy param](#copy-param)
  
  [Move param](#move-param)

  [Create folder param](#create-folder-param)

  [Delete param](#delete-param)

  [Folder struct param](#folder-struct-param)

  [Rename param](#rename-param)
  
Response:

  [Copy result](#copy-result)
  
  [Move result](#move-result)

  [Create folder result](#create-folder-result)

  [Delete result](#delete-result)

  [Folder struct result](#folder-struct-result)

  [Rename result](#rename-result)

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

### Copy result
### Move result
### Create folder result
### Delete result
### Folder struct result
### Rename result
