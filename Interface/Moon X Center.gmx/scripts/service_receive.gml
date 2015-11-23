var map, list = ds_list_create();
var jsonData = string_replace_all(argument0, "\", "/");
map = json_decode(jsonData);
switch(ds_map_find_value(map, "type"))
{
    case "installedApps":
        ds_list_copy(list, ds_map_find_value(map, "data"));
        for(var i=0; i<ds_list_size(list); i++)
        {
            global.installedApps[global.installedAppsCount, 0] = ds_map_find_value(ds_list_find_value(list, i), "name");
            global.installedApps[global.installedAppsCount, 1] = ds_map_find_value(ds_list_find_value(list, i), "path");
            global.installedApps[global.installedAppsCount, 2] = ds_map_find_value(ds_list_find_value(list, i), "imagePath");
            global.installedApps[global.installedAppsCount,3] = sprite_add(global.installedApps[global.installedAppsCount,2], 0, false, false, 0, 0);
            global.installedAppsCount++;
        }
        break;
    case "exit":
        game_end();
        break;
}
ds_list_destroy(list);
ds_map_destroy(map);
