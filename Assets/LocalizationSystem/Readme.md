# Localization System
by CoreTeam Games ver. 1.0

# Intoduction

### File types
There are supported list of file types for localization:
1. Text assets
2. Texture2D
3. AudioClip

# How to localize

## TMP Text
To localize TMP Text assets you can use **Localize TextMeshPro Text** and **Localize TextMeshPro Text Group**. There are differences of this 2 components:

### Localize TextMeshPro Text
This component uses for localize one TMP Text component.
#### How to Use

Add component **CoreTeamGames/Localization/Localize TextMeshPro Text**. There are 2 fields:

|Field Name |Description                                  |
|-----------|---------------------------------------------|
|Line Key   |The key of line used for get localized line  |
|File Name  |The name of file what contains a localization|

### Localize TextMeshPro Text Group
This component uses for localize one TMP Text component.

#### How to Use
Add component **CoreTeamGames/Localization/Localize TextMeshPro Text Group**. There are field:

|Field Name |Description                                  |
|-----------|---------------------------------------------|
|File Name  |The name of file what contains a localization|

And here is list of **Localize TMP_Text group members**. List member contains 2 fields:

|Field Name |Description                                  |
|-----------|---------------------------------------------|
|TMP_Text   |The TMP_Text component where localize line   |
|Line Key   |The key of line used for get localized line  |
