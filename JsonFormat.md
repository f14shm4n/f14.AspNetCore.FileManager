# Json format

## Commands

[CopyCommand](#copycommand)
  
[MoveCommand](#movecommand)

[CreateCommand](#createcommand)

[DeleteCommand](#deletecommand)

[RenameCommand](#renamecommand)

## Queries

[GetFolderStructureQuery](#getfolderstructurequery)
  
### CopyCommand

```
{
  // The path to the directory from which you want to copy data.
  "SourceDirectory": "/", 
  // The path to the directory where you want to paste the data.
  "CurrentFolderPath": "/", 
  // Determines whether to overwrite files and folders.
  "Overwrite": false, 
  // The name of the file or folder that you want to copy.
  "Name": "sample.txt", 
  // Determines whether object is file or folder.
  "IsFile": true 
}
```

### MoveCommand

```
{
  "SourceDirectory": "/", 
  "CurrentFolderPath": "/", 
  "Overwrite": false, 
  "Name": "sample.txt", 
  "IsFile": true 
}
```

### CreateCommand

```
{
  "Name": "new folder name", 
  "CurrentFolderPath": "/"
}
```

### DeleteCommand

```
{
  "TargetName": "fileOrFolderName"
  "CurrentFolderPath": "/"
  "IsFile": true 
}
```

### RenameCommand

```
{
  // Origin object name.
  "OriginName": "old name", 
  // New object name.
  "NewName": "new name", 
  "IsFile": true
  "CurrentFolderPath": "/"
}
```

## Queries

### GetFolderStructureQuery

This is the json format for the folder struct request.

```
{
  // The collection of the file extensions which you want to receive.
  "FileExtensions": [ 
    ".txt" // The file extension.
  ],
  "CurrentFolderPath": "/"
}
```