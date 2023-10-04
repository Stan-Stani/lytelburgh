import { CSSProperties, FC, ReactNode } from 'react'

interface IStackProps {
  flex?: CSSProperties['flex']
  children?: ReactNode
}

export const Stack: FC<IStackProps> = ({ flex, children }) => {
  return <div style={{ display: 'flex', flexDirection: 'column', flex }}>{children}</div>
}
