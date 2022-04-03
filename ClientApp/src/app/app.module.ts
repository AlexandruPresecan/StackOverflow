import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { QuestionsComponent } from './questions/questions.component';
import { UsersComponent } from './users/users.component';
import { QuestionComponent } from './question/question.component';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user/user.component';
import { CreateQuestionComponent } from './create-question/create-question.component';
import { TagsComponent } from './tags/tags.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    QuestionsComponent,
    QuestionComponent,
    CreateQuestionComponent,
    TagsComponent,
    UsersComponent,
    UserComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'questions', component: QuestionsComponent },
      { path: 'questions/:id', component: QuestionComponent },
      { path: 'create-question', component: CreateQuestionComponent },
      { path: 'users', component: UsersComponent },
      { path: 'users/:id', component: UserComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
