with(o_service)
{
    var Buffer = buffer_create( 1024 , buffer_fixed , 1 );
    buffer_seek( Buffer , buffer_seek_start , 0 );
    buffer_write( Buffer , buffer_string , "$" + argument0 + "$" );
    network_send_raw( Socket , Buffer , buffer_tell( Buffer ) );
}
