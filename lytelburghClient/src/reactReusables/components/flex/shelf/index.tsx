import { CSSProperties, FC, ReactNode } from 'react'

interface IShelfProps {
  flex?: CSSProperties['flex']
  children?: ReactNode
}

export const Shelf: FC<IShelfProps> = ({ flex, children }) => {
  return <div style={{ display: 'flex', flexDirection: 'row', flex }}>{children}</div>
}
