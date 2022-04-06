import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
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
import { VotesComponent } from './votes/votes.component';
import { QuestionsPageComponent } from './questions-page/questions-page.component';
import { AnswersComponent } from './answers/answers.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TagsPageComponent } from './tags-page/tags-page.component';
import { TaggedQuestionsPageComponent } from './tagged-questions-page/tagged-questions-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    QuestionsComponent,
    QuestionComponent,
    QuestionsPageComponent,
    CreateQuestionComponent,
    AnswersComponent,
    TagsComponent,
    UsersComponent,
    UserComponent,
    VotesComponent,
    LoginComponent,
    RegisterComponent,
    TagsPageComponent,
    TaggedQuestionsPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'questions', component: QuestionsPageComponent },
      { path: 'questions/:id', component: QuestionComponent },
      { path: 'questions/tagged/:tag', component: TaggedQuestionsPageComponent },
      { path: 'create-question', component: CreateQuestionComponent },
      { path: 'users', component: UsersComponent },
      { path: 'users/:id', component: UserComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'tags', component: TagsPageComponent },
      { path: 'tags/:id', component: TagsPageComponent }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
