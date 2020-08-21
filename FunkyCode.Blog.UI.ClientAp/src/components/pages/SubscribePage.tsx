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
import { ContactDataModel } from '../../model/ContactDataModel';
import { subscribeAction } from '../../redux/Thunks';
import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { FunkyMessage } from '../UIComponents/FunkyMessage';
import { BlogEnginePaths } from '../../config/BlogEngineSettings';
import { Redirect } from 'react-router-dom';
import { resetUiStateAction } from '../../redux/Actions';
import { SubscribeDataModel } from '../../model/SubscribeDataModel';
import { SubscribeDataActionType } from "../../model/SubscribeDataActionType";
import { SubscribeDto } from '../../model/SubscribeDto';
import { SubscriptionResultTypeEnum } from '../../model/SubscriptionResult';

interface Props {
    dispatch: ThunkDispatch<any, any, AnyAction>;
    subscriptionActionResult: SubscriptionResultTypeEnum;
}

interface State  {
    isMsgClosed: boolean;
    action: SubscribeDataActionType
}

const validationSchema = yup.object().shape<SubscribeDataModel>({
    username: yup
      .string()
      .max(30)
      .required(),
    email: yup
      .string()
      .email()
      .required(),
  });

class SubscribePage extends Component<Props & FormikProps<ContactDataModel>, State>  {

    initialValues: SubscribeDataModel = { username: '', email: ''};

    initialValuesUnsubscribe: SubscribeDataModel = { username: 'Unsubscriber', email: ''};


    state : State = { isMsgClosed: false, action: 'unknown'  }

    componentWillMount()
    {

      this.props.dispatch(resetUiStateAction());

    }

    render() {

        if (this.state.isMsgClosed)
        {
            return <Redirect to={ BlogEnginePaths.MainPath} />
        }
       
        const message = this.processMessage(this.props.subscriptionActionResult);
        const isMessageVisible = this.props.subscriptionActionResult !== 'Unknown';

        return <div style={{width: '600px'}}>
        
        <Spacer height={30} />
        <Typography variant="h4">Subscribe</Typography>
        <Spacer height={10} />
        <Typography>You can subscribe to receive information about new blog posts and nothing beside this. You can unsubscribe in any moment from this page or from every email you receive from me.</Typography>

        <Formik
        initialValues={ this.initialValues} 
        onSubmit={this.onSubmitSubscribe.bind(this)}         
        validationSchema={validationSchema}
        >
        {() => (
          
          <Form>
          <Field name="username" label="Username" component={TextField}  variant="filled" />
          <Field name="email" label="Email" component={TextField}  variant="filled"/>
          <Spacer height={15} />
          <FunkyButton buttonType="border" title="Subscribe" onClickEvent={() => {}} submit />
          </Form>

        )}
      </Formik>

      <Spacer height={50} />
      <Typography variant="h4">... or unsubscribe</Typography>
      <Spacer height={10} />
      <Typography>If you are not happy with receiving emails then you can freely unsubscribe.</Typography>
     
      <Formik
        initialValues={ this.initialValuesUnsubscribe} 
        onSubmit={this.onSubmitUnsubscribe.bind(this)}         
        validationSchema={validationSchema}
        >
        {() => (
          
          <Form>
          {/* <Field name="username" label="Username" component={TextField}  variant="filled"  /> */}
          <Field name="email" label="Email" component={TextField}  variant="filled"/>
          <Spacer height={15} />
          <FunkyButton buttonType="border" title="Unsubscribe" onClickEvent={() => { }} submit /> 
          </Form>

        )}
      </Formik>

      

      <FunkyMessage title="Funky Code" message={message} isOpen={isMessageVisible} onClose={this.onClose.bind(this)} />

      </div>
    }

    onSubmitSubscribe(data: SubscribeDataModel) {
        const subscribeDto : SubscribeDto = {...data, action: 'subscribe'};
        this.props.dispatch(subscribeAction(subscribeDto));
    }

    onSubmitUnsubscribe(data: SubscribeDataModel) {
      const subscribeDto : SubscribeDto = {...data, action: 'unsubscribe'};
      this.props.dispatch(subscribeAction(subscribeDto));
    }

    onClose() {
        this.setState({isMsgClosed: true});
    }

    processMessage(substrictionResult: SubscriptionResultTypeEnum) : string
    {
        console.log('subsresult', substrictionResult);
        if (substrictionResult === 'Subscribed') return "You have been added to Funky Code blog subscribers.";
        if (substrictionResult === 'Unsubscribed') return "You are no longer Funky Code blog subscriber.";
        if (substrictionResult === 'AlreadySubscribed') return "You have been already subscribed.";
        if (substrictionResult === 'NotInDatabase') return "You weren't present in our DB therefore you weren't unsubscribed.";
        return "";
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
    subscriptionActionResult: state.subscriptionActionStatus
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(SubscribePage);