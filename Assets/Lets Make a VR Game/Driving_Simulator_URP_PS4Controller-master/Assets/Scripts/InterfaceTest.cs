using System.Collections;
using System.Collections.Generic;


public interface moveForward<T,K>
{
    T speed { get; set; }
    K forceForward();
}
