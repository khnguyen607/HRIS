import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TechnicalReqsesComponent} from './technicalReqses.component';



const routes: Routes = [
    {
        path: '',
        component: TechnicalReqsesComponent,
        pathMatch: 'full'
    },
    
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TechnicalReqsRoutingModule {
}
