import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RecruitmentsComponent} from './recruitments.component';



const routes: Routes = [
    {
        path: '',
        component: RecruitmentsComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RecruitmentRoutingModule {
}
