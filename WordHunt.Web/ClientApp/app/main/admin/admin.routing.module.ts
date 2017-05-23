import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminComponent } from './admin.component';
import { WordsComponent } from './words/word.component';
import { CategoriesComponent } from './categories/categories.component';

const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            {
                path: '',
                redirectTo: 'words'
            },
            {
                path: 'words',
                component: WordsComponent
            },
            {
                path: 'categories',
                component: CategoriesComponent
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }

export const routableComponents = [
    AdminComponent,
    WordsComponent,
    CategoriesComponent
];