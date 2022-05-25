import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { VotesComponent } from './components/votes/votes.component';
import { QuestionsComponent } from './components/questions/questions.component';
import { QuestionsPageComponent } from './pages/questions-page/questions-page.component';
import { CreateQuestionPageComponent } from './pages/create-question-page/create-question-page.component';
import { CreateAnswerComponent } from './components/create-answer/create-answer.component';
import { TagsComponent } from './components/tags/tags.component';
import { UsersPageComponent } from './pages/users-page/users-page.component';
import { UserPageComponent } from './pages/user-page/user-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { TagsPageComponent } from './pages/tags-page/tags-page.component';
import { TaggedQuestionsPageComponent } from './pages/tagged-questions-page/tagged-questions-page.component';
import { QuestionPageComponent } from './pages/question-page/question-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { AnswerVotesComponent } from './components/votes/answer-votes.component';
import { QuestionVotesComponent } from './components/votes/question-votes.component';
import { AccountPageComponent } from './pages/account-page/account-page.component';
import { AnswerComponent } from './components/answer/answer.component';
import { AnswersComponent } from './components/answers/answers.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    VotesComponent,
    QuestionsComponent,
    QuestionsPageComponent,
    CreateQuestionPageComponent,
    CreateAnswerComponent,
    AnswersComponent,
    AnswerComponent,
    TagsComponent,
    UsersPageComponent,
    UserPageComponent,
    LoginPageComponent,
    RegisterPageComponent,
    TagsPageComponent,
    TaggedQuestionsPageComponent,
    QuestionPageComponent,
    HomePageComponent,
    AnswerVotesComponent,
    QuestionVotesComponent,
    AccountPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomePageComponent, pathMatch: 'full' },
      { path: 'questions', component: QuestionsPageComponent },
      { path: 'questions/:id', component: QuestionPageComponent },
      { path: 'questions/tagged/:tag', component: TaggedQuestionsPageComponent },
      { path: 'create-question', component: CreateQuestionPageComponent },
      { path: 'users', component: UsersPageComponent },
      { path: 'users/:id', component: UserPageComponent },
      { path: 'register', component: RegisterPageComponent },
      { path: 'login', component: LoginPageComponent },
      { path: 'account', component: AccountPageComponent },
      { path: 'tags', component: TagsPageComponent }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
