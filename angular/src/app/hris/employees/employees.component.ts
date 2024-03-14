﻿import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { EmployeesServiceProxy, EmployeeDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditEmployeeModalComponent } from './create-or-edit-employee-modal.component';

import { ViewEmployeeModalComponent } from './view-employee-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './employees.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class EmployeesComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditEmployeeModal', { static: true }) createOrEditEmployeeModal: CreateOrEditEmployeeModalComponent;
    @ViewChild('viewEmployeeModal', { static: true }) viewEmployeeModal: ViewEmployeeModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    fullNameFilter = '';
    addressFilter = '';
    phoneFilter = '';
    departmentFilter = '';
    jobTitleFilter = '';
    socialSecurityFilter = '';
        employeeWorkTypeTypeFilter = '';






    constructor(
        injector: Injector,
        private _employeesServiceProxy: EmployeesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getEmployees(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._employeesServiceProxy.getAll(
            this.filterText,
            this.fullNameFilter,
            this.addressFilter,
            this.phoneFilter,
            this.departmentFilter,
            this.jobTitleFilter,
            this.socialSecurityFilter,
            this.employeeWorkTypeTypeFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createEmployee(): void {
        this.createOrEditEmployeeModal.show();        
    }


    deleteEmployee(employee: EmployeeDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._employeesServiceProxy.delete(employee.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._employeesServiceProxy.getEmployeesToExcel(
        this.filterText,
            this.fullNameFilter,
            this.addressFilter,
            this.phoneFilter,
            this.departmentFilter,
            this.jobTitleFilter,
            this.socialSecurityFilter,
            this.employeeWorkTypeTypeFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    

    resetFilters(): void {
        this.filterText = '';
            this.fullNameFilter = '';
    this.addressFilter = '';
    this.phoneFilter = '';
    this.departmentFilter = '';
    this.jobTitleFilter = '';
    this.socialSecurityFilter = '';
		this.employeeWorkTypeTypeFilter = '';
					
        this.getEmployees();
    }
}
