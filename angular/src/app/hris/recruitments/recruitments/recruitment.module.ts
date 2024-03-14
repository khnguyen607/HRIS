import {NgModule} from '@angular/core';
import {AppSharedModule} from '@app/shared/app-shared.module';
import {AdminSharedModule} from '@app/admin/shared/admin-shared.module';
import {RecruitmentRoutingModule} from './recruitment-routing.module';
import {RecruitmentsComponent} from './recruitments.component';
import {CreateOrEditRecruitmentModalComponent} from './create-or-edit-recruitment-modal.component';
import {ViewRecruitmentModalComponent} from './view-recruitment-modal.component';


            import {MasterDetailChild_Recruitment_RecruitmentCandidateModule} from '../recruitmentCandidates/masterDetailChild_Recruitment_recruitmentCandidate.module';

@NgModule({
    declarations: [
        RecruitmentsComponent,
        CreateOrEditRecruitmentModalComponent,
        ViewRecruitmentModalComponent,
        
    ],
    imports: [AppSharedModule, RecruitmentRoutingModule , AdminSharedModule ,
            MasterDetailChild_Recruitment_RecruitmentCandidateModule],
    
})
export class RecruitmentModule {
}
