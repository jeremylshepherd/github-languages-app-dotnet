import React from 'react'
import '../Card.css'

function Card(props: any) {
    const width = { width: `${props.width}px` };
    return (
        <div className="language_card" style={ width }>
            {props.children}
        </div>
    )
}

export default Card