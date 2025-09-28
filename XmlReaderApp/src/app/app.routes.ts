import { Routes } from '@angular/router';

export const routes: Routes = [
    {
    path: 'mail-content',
    loadComponent: () =>
      import('./mail-content/mail-content').then(m => m.MailContent)
  }
];
