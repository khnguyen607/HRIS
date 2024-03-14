import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';

import {MasterDetailChild_Recruitment_RecruitmentCandidatesComponent} from './masterDetailChild_Recruitment_recruitmentCandidates.component';
import {MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent} from './masterDetailChild_Recruitment_create-or-edit-recruitmentCandidate-modal.component';
import {MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent} from './masterDetailChild_Recruitment_view-recruitmentCandidate-modal.component';



@NgModule({
    declarations: [
        MasterDetailChild_Recruitment_RecruitmentCandidatesComponent,
        MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent,
        MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent,
        
    ],
    imports: [AppSharedModule , AdminSharedModule ],
    
			    exports: [
                    MasterDetailChild_Recruitment_RecruitmentCandidatesComponent,
                    MasterDetailChild_Recruitment_CreateOrEditRecruitmentCandidateModalComponent,
                    MasterDetailChild_Recruitment_ViewRecruitmentCandidateModalComponent,
                ]
			
})
export class MasterDetailChild_Recruitment_RecruitmentCandidateModule {
}
