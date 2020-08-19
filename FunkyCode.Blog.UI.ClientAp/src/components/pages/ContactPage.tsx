import React, { Component } from 'react';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';
import { TextField } from 'material-ui-formik-components/TextField'
import * as yup from 'yup';
import { Formik, FormikProps, Form, Field } from 'formik';
import { Typography } from '@material-ui/core';
import { Spacer } from '../Spacer';
import { FunkyButton } from '../UIComponents/FunkyButton';
import { isNullOrUndefined } from 'util';
import { ContactDataModel } from '../../model/ContactDataModel';
import { postContactMessage } from '../../redux/Thunks';
import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { FunkyMessage } from '../UIComponents/FunkyMessage';
import { BlogEnginePaths } from '../../config/BlogEngineSettings';
import { Redirect } from 'react-router-dom';
import { resetUiStateAction } from '../../redux/Actions';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
    isContactMessagePosted: boolean;
}

interface State  {
    isMsgClosed: boolean;
}

const validationSchema = yup.object().shape<ContactDataModel>({
    username: yup
      .string()
      .max(30)
      .required(),
    email: yup
      .string()
      .email()
      .required('Required!'),
    subject: yup
      .string()
      .max(30)
      .required('Required!'),
    message: yup
      .string()
      .required('Required!')
      .test('message-test', 'Message should be longer than Subject', function(value) {
        let { subject } = this.parent;
        if (isNullOrUndefined(value) || isNullOrUndefined(subject)) return false;
        let isGreater = value.length > subject.length;
        return isGreater;
      })
      .test('message-test-bad-language', 'Message should not contain bad language', function(value) {
        if (isNullOrUndefined(value)) return true;
        if (value.includes('shit')) return false;
        return true;
      })

  });

class ContactPage extends Component<Props & FormikProps<ContactDataModel>, State>  {

    initialValues: ContactDataModel = { username: '', email: '', subject: '', message: '' };

    state = { isMsgClosed: false }

    componentWillMount()
    {

      this.props.dispatch(resetUiStateAction());

    }

    render() {

        if (this.state.isMsgClosed)
        {
            return <Redirect to={ BlogEnginePaths.MainPath} />
        }

        return <div style={{width: '600px'}}>
        
        <Spacer height={30} />
        <Typography variant="h4">Contact</Typography>

        <Formik
        initialValues={ this.initialValues} 
        onSubmit={this.onSubmit.bind(this)}         
        validationSchema={validationSchema}
        >
        {() => (
          
          <Form>
          <Field name="username" label="Username" component={TextField}  variant="filled" />
          <Field name="email" label="Email" component={TextField}  variant="filled"/>
          <Field name="subject" label="Subject" component={TextField}  variant="filled"/>
          <Field name="message" label="Message" multiline component={TextField}  variant="filled" rows={4}/>
          <Spacer height={20} />
          <FunkyButton buttonType="border" title="Submit" onClickEvent={() => {}} submit /> 
          </Form>

        )}
      </Formik>

      <FunkyMessage title="Funky Code" message="Message was sent!" isOpen={this.props.isContactMessagePosted} onClose={this.onClose.bind(this)} />

      </div>
    }

    onSubmit(data: ContactDataModel) {
        this.props.dispatch(postContactMessage(data));
    }

    onClose() {
        this.setState({isMsgClosed: true});
    }

}


const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AnyAction>) => {
     return {
         dispatch
     };
 };
  
const mapStateToProps = (store: IAppState) => {

  const state: IFunkyState = store.funkyState;

  return {
      isContactMessagePosted: state.isContactMessagePosted
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(ContactPage);