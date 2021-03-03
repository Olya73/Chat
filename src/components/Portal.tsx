import {Component} from 'react'
import ReactDOM from 'react-dom'


class Portal extends Component<{ children: React.ReactNode}> {
    el: HTMLDivElement = document.createElement('div');    
  
    componentDidMount(): void {
      document.body.appendChild(this.el);
    }
  
    componentWillUnmount(): void {
      document.body.removeChild(this.el);
    }
  
    render() {
      return ReactDOM.createPortal(
        this.props.children,
        this.el
      );
    }
  }

  export default Portal;