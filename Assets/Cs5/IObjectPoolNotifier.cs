using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolNotifier
{
    // Туындаған жағдайда объект пулына қайтады
    void OnEnqueuedToPool();
    /* Шақырылады, қашан нысан тастап пулы немесе жай ғана құрылды. Егер 'created' мәні true, 
     * * онда объект ғана құрылды және өңделмейді.*/

    /*  Осылайша сіз нысанды теңшеу үшін бір әдісті қолданасыз, 
     * бірінші рет және барлық кейінгі рет.))*/
    void OnCreatedOrDequeuedFromPool(bool created);
}
