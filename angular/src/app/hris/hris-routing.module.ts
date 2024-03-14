import { NgModule } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'recruitments',
                        loadChildren: () => import('./recruitments/recruitments/recruitment.module').then(m => m.RecruitmentModule),
                        data: { permission: 'Pages.Recruitments' }
                    },
                    {
                        path: 'technicalReqses',
                        loadChildren: () => import('./technicalReqses/technicalReqs.module').then(m => m.TechnicalReqsModule),
                        data: { permission: 'Pages.TechnicalReqses' }
                    },
                    {
                        path: 'basicReqses',
                        loadChildren: () => import('./basicReqses/basicReqs.module').then(m => m.BasicReqsModule),
                        data: { permission: 'Pages.BasicReqses' }
                    },
                    {
                        path: 'employees',
                        loadChildren: () => import('./employees/employee.module').then(m => m.EmployeeModule),
                        data: { permission: 'Pages.Employees' }
                    },
                    { path: '', redirectTo: 'hostDashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'hostDashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class HrisRoutingModule {
    constructor(private router: Router) {
        router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                window.scroll(0, 0);
            }
        });
    }
}
