var resultMap = ds_map_create();
var json = string_replace_all(argument0,"'","");
resultMap = json_decode(json);
//show_message(string(ds_map_find_value(resultMap, "type")));
show_message(string(ds_map_find_value(ds_list_find_value(resultMap,0),"name")));
//string(ds_map_find_value(ds_list_find_value(resultMap,0),"InstalledApps"))
//show_message(
    //string(ds_map_find_value(ds_list_find_value(ds_map_find_value(resultMap, "InstalledApps"),0),"name"))
    //ds_map_find_first(resultMap)
//);
ds_map_clear(resultMap);
