import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { RecruitmentsServiceProxy, RecruitmentDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditRecruitmentModalComponent } from './create-or-edit-recruitment-modal.component';

import { ViewRecruitmentModalComponent } from './view-recruitment-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './recruitments.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class RecruitmentsComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditRecruitmentModal', { static: true }) createOrEditRecruitmentModal: CreateOrEditRecruitmentModalComponent;
    @ViewChild('viewRecruitmentModal', { static: true }) viewRecruitmentModal: ViewRecruitmentModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    titleFilter = '';
    jdFilter = '';




            recruitmentCandidateRowSelection: boolean[] = [];
            

                   childEntitySelection: {} = {};
            

    constructor(
        injector: Injector,
        private _recruitmentsServiceProxy: RecruitmentsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getRecruitments(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._recruitmentsServiceProxy.getAll(
            this.filterText,
            this.titleFilter,
            this.jdFilter,
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

    createRecruitment(): void {
        this.createOrEditRecruitmentModal.show();        
    }


    deleteRecruitment(recruitment: RecruitmentDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._recruitmentsServiceProxy.delete(recruitment.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    
    
                  selectEditTable(table){
                      this.childEntitySelection = {};
                      this.childEntitySelection[table] = true;
                  }
            
    
               openChildRowForRecruitmentCandidate(index, table) {
                   let isAlreadyOpened = this.recruitmentCandidateRowSelection[index];                   
                   this.closeAllChildRows();                   
                   this.recruitmentCandidateRowSelection = [];
                   if (!isAlreadyOpened) {
                       this.recruitmentCandidateRowSelection[index] = true;
                   }
                   this.selectEditTable(table);
               }
            
    
                  closeAllChildRows() : void{
                     
                this.recruitmentCandidateRowSelection = [];
            
                  }
    

    resetFilters(): void {
        this.filterText = '';
            this.titleFilter = '';
    this.jdFilter = '';

        this.getRecruitments();
    }
}
