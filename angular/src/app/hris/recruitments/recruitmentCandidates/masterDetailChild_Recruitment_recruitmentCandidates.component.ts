import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild, Input } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { RecruitmentCandidatesServiceProxy, RecruitmentCandidateDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent } from './masterDetailChild_Recruitment_create-or-edit-recruitmentCandidate-modal.component';

import { MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent } from './masterDetailChild_Recruitment_view-recruitmentCandidate-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

             import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './masterDetailChild_Recruitment_recruitmentCandidates.component.html',
    selector: "masterDetailChild_Recruitment_recruitmentCandidates-component",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class MasterDetailChild_Recruitment_RecruitmentCandidatesComponent extends AppComponentBase {
    @Input("recruitmentId") recruitmentId: any;
    
    @ViewChild('createOrEditRecruitmentCandidateModal', { static: true }) createOrEditRecruitmentCandidateModal: MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent;
    @ViewChild('viewRecruitmentCandidateModalComponent', { static: true }) viewRecruitmentCandidateModal: MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    fullNameFilter = '';
    maxAgeFilter : number;
		maxAgeFilterEmpty : number;
		minAgeFilter : number;
		minAgeFilterEmpty : number;
    phoneFilter = '';
    emailFilter = '';
    cvFilter = '';
    maxPointFilter : number;
		maxPointFilterEmpty : number;
		minPointFilter : number;
		minPointFilterEmpty : number;
    maxRecruitmentIdFilter : number;
		maxRecruitmentIdFilterEmpty : number;
		minRecruitmentIdFilter : number;
		minRecruitmentIdFilterEmpty : number;




    constructor(
        injector: Injector,
        private _recruitmentCandidatesServiceProxy: RecruitmentCandidatesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
             private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getRecruitmentCandidates(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records &&
                this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._recruitmentCandidatesServiceProxy.getAll(
            this.filterText,
            this.fullNameFilter,
            this.maxAgeFilter == null ? this.maxAgeFilterEmpty: this.maxAgeFilter,
            this.minAgeFilter == null ? this.minAgeFilterEmpty: this.minAgeFilter,
            this.phoneFilter,
            this.emailFilter,
            this.cvFilter,
            this.maxPointFilter == null ? this.maxPointFilterEmpty: this.maxPointFilter,
            this.minPointFilter == null ? this.minPointFilterEmpty: this.minPointFilter,
            this.maxRecruitmentIdFilter == null ? this.maxRecruitmentIdFilterEmpty: this.maxRecruitmentIdFilter,
            this.minRecruitmentIdFilter == null ? this.minRecruitmentIdFilterEmpty: this.minRecruitmentIdFilter,
            // undefined,
            this.recruitmentId,
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

    createRecruitmentCandidate(): void {
        this.createOrEditRecruitmentCandidateModal.show(this.recruitmentId);        
    }


    deleteRecruitmentCandidate(recruitmentCandidate: RecruitmentCandidateDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._recruitmentCandidatesServiceProxy.delete(recruitmentCandidate.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    
}
