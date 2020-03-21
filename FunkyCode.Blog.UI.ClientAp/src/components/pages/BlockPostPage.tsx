import React, { Component } from 'react';
import { connect } from 'react-redux';

import { IAppState } from '../../redux/Store';
import { IFunkyState } from '../../redux/State';
import { isNullOrUndefined } from 'util';
import { BlogPost } from '../../model/BlogPost';

import Prism from "prismjs"

// @ts-ignore;
import csharp from 'prismjs/components/prism-csharp';
// @ts-ignore;
import sql from 'prismjs/components/prism-sql';

interface Props  {
  post?: BlogPost
}

interface State {

}

class BlockPostPage extends Component<Props, State>  {

  
  componentDidMount()
  {
    // this is temporary workaround for receiving 
    console.log('csharp',csharp, sql);
  }
 

  componentDidUpdate()
  {

    console.log('Prism.highlightAll();');
    
    Prism.highlightAll();
    console.log(Prism.languages);
    
    
  }

  render() {

    const { post } = this.props;

    if (isNullOrUndefined(post))
      return null;


    // this works
    const source = post.content;
    
    if (isNullOrUndefined(source))
      return null;

    // return <pre>
    //   <code className='language-csharp'>
    //   {`public void OnLogging(Customer customer)
    //     {
    //         // (...)
      
    //         if (customer.Accounts.Count >= 10)
    //             ConvertToPremiumCustomer(customer);
      
    //         // (...)
    //     }
    //   `}
    //   </code>
    // </pre>
      

    return  <div style={{display: 'block', overflowX: 'auto', overflowY: 'auto', height: '100%', paddingRight: '20%' }}>
        <div dangerouslySetInnerHTML={this.createMarkup(source)}  />
    </div>
    
    
  }

  createMarkup(content: string)
  {
    return {__html: content};

  }

  

}

const mapDispatchToProps = () => {
  return {
  
  };
};

const mapStateToProps = (store: IAppState) => {
  
  const state : IFunkyState = store.funkyState;
  
  return {
    post: state.currentPost
  };

};

export default connect(mapStateToProps, mapDispatchToProps)(BlockPostPage);




