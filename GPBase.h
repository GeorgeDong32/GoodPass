#ifndef __GPBASE_H__
#define __GPBASE_H__
/*********************************************************
*                                                        *
* GPBase.h -- Provides the base functions for GoodPass   *
* Copyright(c) GeorgeDong32(Github).All rights reserved. *
* GPBase.h version 2.5.0 dev                             *
*                                                        *
**********************************************************/
#include "Data.h"
int start_option(char control);

void GP_add(Manager& manager);

void GP_search(Manager& manager);

void GP_get(Manager& manager);

void GP_change(Manager& manager);

void GP_delete(Manager& manager);

string Getsystime();

#endif