var map, list = ds_list_create();
map = json_decode(argument0);
switch(ds_map_find_value(map, "type"))
{
    case "InstalledApps":
        ds_list_copy(list, ds_map_find_value(map, "data"));
        for(var i=0; i<ds_list_size(list); i++)
        {
            var app = ds_map_create();
            ds_map_add(app, "name", ds_map_find_value(ds_list_find_value(list, i), "name"));
            ds_map_add(app, "path", ds_map_find_value(ds_list_find_value(list, i), "path"));
            ds_map_add(app, "imagePath", ds_map_find_value(ds_list_find_value(list, i), "imagePath"));
            global.installedApps[global.installedAppsCount] = app;
        }
        //show_message(ds_map_find_value(ds_list_find_value(list, 0), "name"));
        break;
}
ds_list_destroy(list);
ds_map_destroy(map);
