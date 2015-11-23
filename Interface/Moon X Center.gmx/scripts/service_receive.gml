var map;
map = json_decode(argument0);
switch(ds_map_find_value(map, "type"))
{
    case "InstalledApps":
        var list = ds_list_create();
        ds_list_copy(list, ds_map_find_value(map, "data"));
        //show_message(ds_map_find_value(ds_list_find_value(list, 0), "name"));
        break;
}
ds_list_clear(list);
ds_map_clear(map);
